namespace SDPhotosDownloader;

public interface IFileSystem
{
    IEnumerable<string> GetFiles(string directoryPath);
    
    IEnumerable<string> GetDirectories(string directoryPath);
    
    FileEntry GetFileEntry(string path);
}