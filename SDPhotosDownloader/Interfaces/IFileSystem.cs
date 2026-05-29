namespace SDPhotosDownloader;

public interface IFileSystem
{
    /// <summary>
    /// Returns the full paths of all files in the specified directory.
    /// </summary>
    IEnumerable<string> GetFiles(string directoryPath);
    
    /// <summary>
    /// Returns the full paths of all subdirectories in the specified directory.
    /// </summary>
    IEnumerable<string> GetDirectories(string directoryPath);
    
    /// <summary>
    /// Returns a FileEntry object containing information about the file at the specified path.
    /// </summary>
    FileEntry GetFileEntry(string path);
}