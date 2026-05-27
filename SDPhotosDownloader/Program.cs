// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using SDPhotosDownloader;

var rootCommand = new RootCommand("Available commands for SDPhotosDownloader:")
{
    CLOptions.SrcOption,
    CLOptions.DestOption,
    CLOptions.DateOption,
    CLOptions.IsOverwrite
};

var parseResult = rootCommand.Parse(args);

parseResult.GetValue(CLOptions.SrcOption);

var fileSystem = new FileSystemAdapter();
var consoleWriter = new ConsoleWriter();
var options = new DownloadOptions(
    parseResult.GetValue(CLOptions.SrcOption)??string.Empty,
    parseResult.GetValue(CLOptions.DestOption)??string.Empty,
    parseResult.GetValue(CLOptions.DateOption),
    parseResult.GetValue(CLOptions.IsOverwrite)
);
var service = new DownloadService(options, fileSystem, consoleWriter);
return service.Start();