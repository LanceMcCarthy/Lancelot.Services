using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FileWatcher.Service
{
    public class FileMirrorWorker : BackgroundService
    {
        private readonly ILogger<FileMirrorWorker> _logger;
        private readonly CommandLineOptions _options;

        public FileMirrorWorker(ILogger<FileMirrorWorker> logger, CommandLineOptions options)
        {
            _logger = logger;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("FileMirrorWorker Service started");

            if (!Directory.Exists(_options.SourcePath))
            {
                _logger.LogError($"Directory \"{_options.SourcePath}\" does not exist.");
                return;
            }

            if (!Directory.Exists(_options.DestinationPath))
            {
                Directory.CreateDirectory(_options.DestinationPath);
                _logger.LogError($"Directory \"{_options.DestinationPath}\" created.");
            }
            
            await ProcessAllExistingFilesAsync();
            
            using var watcher = new FileSystemWatcher
            {
                Path = _options.SourcePath,
                IncludeSubdirectories = _options.IsRecursive
            };

            _logger.LogInformation($"Listening for new files in \"{_options.SourcePath}\"...");

            watcher.Created += async (s, e) =>
            {
                _logger.LogInformation($"New file detected: {e.FullPath}, processing...");

                await Task.Delay(1000, stoppingToken).ContinueWith(async task => await ProcessFileAsync(e.FullPath), stoppingToken);
            };

            watcher.EnableRaisingEvents = true;

            var tcs = new TaskCompletionSource<bool>();
            stoppingToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            await tcs.Task;

            _logger.LogInformation("Service stopped");
        }

        private async Task ProcessAllExistingFilesAsync()
        {
            _logger.LogInformation($"Processing all existing files in source folder...");

            var copiedCount = 0;
            var skippedCount = 0;
            var errorCount = 0;

            foreach (var filePath in Directory.EnumerateFiles(_options.SourcePath, $"*.*", SearchOption.AllDirectories))
            {
                var result = await ProcessFileAsync(filePath);

                switch (result)
                {
                    case FileProcessResult.Copied:
                        copiedCount++;
                        break;
                    case FileProcessResult.Skipped:
                        skippedCount++;
                        break;
                    case FileProcessResult.Error:
                        errorCount++;
                        break;
                }
            }

            _logger.LogInformation($"ProcessAll Result - Copied: {copiedCount}, Skipped: {skippedCount}, Failed: {errorCount} (see Error logs for details).");
        }

        private Task<FileProcessResult> ProcessFileAsync(string filePath)
        {
            try
            {
                var updatedPath = filePath.Replace(_options.SourcePath, _options.DestinationPath);

                var targetDirectory = Path.GetDirectoryName(updatedPath);

                if (string.IsNullOrEmpty(targetDirectory))
                {
                    _logger.LogError($"Target Directory Name is Null.");
                    return Task.FromResult(FileProcessResult.Error);
                }

                if(!Directory.Exists(targetDirectory))
                {
                    Directory.CreateDirectory(targetDirectory);
                }

                _logger.LogInformation($"ProcessFileAsync (Source: {filePath}, Destination: {updatedPath})");

                if (File.Exists(updatedPath) && !_options.OverwriteDestination)
                {
                    return Task.FromResult(FileProcessResult.Skipped);
                }
                else
                {
                    File.Copy(filePath, updatedPath, _options.OverwriteDestination);

                    return Task.FromResult(FileProcessResult.Copied);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing file {filePath}");
            }

            return Task.FromResult(FileProcessResult.Error);
        }
    }
}