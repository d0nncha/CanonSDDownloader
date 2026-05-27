namespace SDPhotosDownloader;

public interface IDownloadOptions
{
    public string RootPath { get; }
    
    public string DestPath { get; }
    
    public DateTime DateFrom { get; }
    
    public bool IsOverwrite { get; }
}