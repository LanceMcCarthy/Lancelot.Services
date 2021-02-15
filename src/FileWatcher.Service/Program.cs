﻿using System.Collections.Generic;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventLog;
using System.Threading.Tasks;

namespace FileWatcher.Service
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            return await Parser.Default.ParseArguments<CommandLineOptions>(args)
                .MapResult(async (opts) =>
                    {
                        await CreateHostBuilder(args, opts).Build().RunAsync();
                        return 0;
                    },
                    errs => Task.FromResult(-1)); // Invalid arguments
        }

        public static IHostBuilder CreateHostBuilder(string[] args, CommandLineOptions opts)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureLogging(configureLogging => configureLogging.AddFilter<EventLogLoggerProvider>(level => level >= LogLevel.Information))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(opts);
                    services.AddHostedService<FileMirrorWorker>()
                        .Configure<EventLogSettings>(config =>
                        {
                            config.LogName = "File Watcher Service";
                            config.SourceName = "File Watcher Service Source";
                        });
                })
                .UseWindowsService();
        }
    }
}
