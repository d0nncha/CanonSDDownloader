namespace SDPhotosDownloader;

public interface IScanResult
{
    IDictionary<string, IList<FileEntry>> Groups { get; }
    
    int FilesCount { get; }
    
    double TotalSizeMb { get; }
}