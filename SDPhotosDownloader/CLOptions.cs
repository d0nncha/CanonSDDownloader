﻿using System.CommandLine;

namespace SDPhotosDownloader;

public static class CLOptions
{
    public static readonly Option<string> SrcOption = new (
        "--src", 
        "Full path where stored raw photos on sd-card")
        {IsRequired = true};
    
    public static readonly Option<string> DestOption = new (
        "--dest", 
        "Destination root folder for raw photos");
    
    public static readonly Option<DateTime> DateOption = new (
        "--date", 
        () => DateTime.MaxValue,
        "The date from which the photos should be uploaded");
}