namespace SDPhotosDownloader;

public interface IScanResult
{
    /// <summary>
    /// Gets a dictionary where the key is the file name
    /// and the value is a list of FileEntry objects representing files with that name.
    /// </summary>
    IDictionary<string, IList<FileEntry>> Groups { get; }
    
    /// <summary>
    /// Gets the total number of files found in the scan.
    /// </summary>
    int FilesCount { get; }
    
    /// <summary>
    /// Gets the total size of all files found in the scan, in megabytes.
    /// </summary>
    double TotalSizeMb { get; }
}