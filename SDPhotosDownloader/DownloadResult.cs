namespace SDPhotosDownloader;

public class DownloadResult
{
    public long TotalProcessed { get; set; }
    
    public long TotalDownloaded { get; set; }
    
    public long Skipped { get; set; }
    
    public long Overwritten { get; set; }
}