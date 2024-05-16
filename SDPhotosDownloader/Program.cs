// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using SDPhotosDownloader;

var srcOption = new Option<string>(
    "--src", 
    "Full path where stored raw photos on sd-card");
var destOption = new Option<string>(
    "--dest", 
    "Destination root folder for raw photos");
var dateOption = new Option<DateTime?>(
    "--date", 
    "The date from which the photos should be uploaded");

var rootCommand = new RootCommand("Available commands for SDPhotosDownloader:");
rootCommand.AddOption(srcOption);
rootCommand.AddOption(destOption);
rootCommand.AddOption(dateOption);

rootCommand.SetHandler(
    Downloader.Start, 
    srcOption, 
    destOption, 
    dateOption);

return rootCommand.Invoke(args);