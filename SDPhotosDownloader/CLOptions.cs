using System.CommandLine;

namespace SDPhotosDownloader;

public static class CLOptions
{
    public static Option<string> SrcOption = new Option<string>(
        "--src", 
        "Full path where stored raw photos on sd-card");
    public static Option<string> DestOption = new Option<string>(
        "--dest", 
        "Destination root folder for raw photos");
    public static Option<DateTime?> DateOption = new Option<DateTime?>(
        "--date", 
        "The date from which the photos should be uploaded");
}