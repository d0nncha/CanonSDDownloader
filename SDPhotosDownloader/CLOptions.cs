using System.CommandLine;

namespace SDPhotosDownloader;

public static class CLOptions
{
    public static readonly Option<string> SrcOption = new ("--src")
    {
        Description = "Full path where stored raw photos on sd-card"
    };
    
    public static readonly Option<string> DestOption = new ("--dest")
    {
        Description = "Destination root folder for raw photos"
    };
    
    public static readonly Option<DateTime> DateOption = new ("--date")
    {
        Description = "The date from which the photos should be uploaded"
    };

    public static readonly Option<bool> IsOverwrite = new("--overwrite")
    {
        Description = "Overwrite existing files"
    };
}