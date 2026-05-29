namespace SDPhotosDownloader;

public interface IDownloadOptions
{
    /// <summary>
    /// Root path to scan for photos. It can be a local path or a network share (e.g. \\server\share).
    /// </summary>
    public string SrcPath { get; }
    
    /// <summary>
    /// Root path to save downloaded photos. It can be a local path or a network share (e.g. \\server\share).
    /// </summary>
    public string DestPath { get; }
    
    /// <summary>
    /// Only photos with creation date greater than or equal to this date will be downloaded.
    /// </summary>
    public DateTime DateFrom { get; }
    
    /// <summary>
    /// If true, existing files in the destination folder will be overwritten. If false, existing files will be skipped.
    /// </summary>
    public bool IsOverwrite { get; }
}