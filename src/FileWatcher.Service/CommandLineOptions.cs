using CommandLine;

namespace FileWatcher.Service
{
    public class CommandLineOptions
    {
        [Value(index: 0, Required = true, HelpText = "Folder to watch.")]
        public string SourcePath { get; set; }

        [Value(index: 1, Required = true, HelpText = "Copy destination folder")]
        public string DestinationPath { get; set; }

        [Option(shortName: 'r', longName: "recursive", Required = false, HelpText = "Include subfolders", Default = true)]
        public bool IsRecursive { get; set; }

        [Option(shortName: 'o', longName: "overwrite", Required = false, HelpText = "Always overwrite existing file (false=skip)", Default = false)]
        public bool OverwriteDestination { get; set; }
    }
}