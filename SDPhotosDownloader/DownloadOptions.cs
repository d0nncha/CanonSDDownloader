namespace SDPhotosDownloader;

public class DownloadOptions : IDownloadOptions
{
    public DownloadOptions(string srcPath, string destPath, DateTime dateFrom, bool isOverwrite)
    {
        SrcPath = srcPath;
        DestPath = destPath;
        DateFrom = dateFrom;
        IsOverwrite = isOverwrite;
    }
    
    public string SrcPath { get; }
    
    public string DestPath { get; }
    
    public DateTime DateFrom { get; }
    
    public bool IsOverwrite { get; }
}