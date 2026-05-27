namespace SDPhotosDownloader;

public class ScanResult : IScanResult
{
    public ScanResult(IDictionary<string, IList<FileEntry>> groups, int filesCount, double totalSizeMb)    
    {
        Groups = groups;
        FilesCount = filesCount;
        TotalSizeMb = totalSizeMb;
    }
    
    public IDictionary<string, IList<FileEntry>> Groups { get; }
    
    public int FilesCount { get; }
    
    public double TotalSizeMb { get; }
}