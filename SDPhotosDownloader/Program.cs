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

rootCommand.SetHandler(
    Downloader.Start, 
    CLOptions.SrcOption, 
    CLOptions.DestOption, 
    CLOptions.DateOption,
    CLOptions.IsOverwrite);

return rootCommand.Invoke(args);