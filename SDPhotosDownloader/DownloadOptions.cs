namespace SDPhotosDownloader;

public class DownloadOptions : IDownloadOptions
{
    public DownloadOptions(string rootPath, string destPath, DateTime dateFrom, bool isOverwrite)
    {
        RootPath = rootPath;
        DestPath = destPath;
        DateFrom = dateFrom;
        IsOverwrite = isOverwrite;
    }
    
    public string RootPath { get; }
    
    public string DestPath { get; }
    
    public DateTime DateFrom { get; }
    
    public bool IsOverwrite { get; }
}