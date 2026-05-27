namespace SDPhotosDownloader;

public class FileSystemAdapter : IFileSystem
{
    public IEnumerable<string> GetFiles(string directoryPath)
    {
        return Directory.GetFiles(directoryPath);
    }

    public IEnumerable<string> GetDirectories(string directoryPath)
    {
        return Directory.GetDirectories(directoryPath);
    }

    public FileEntry GetFileEntry(string path)
    {
        var fileInfo = new FileInfo(path);
        return new FileEntry(fileInfo.FullName, fileInfo.Name, fileInfo.CreationTime, fileInfo.Length);
    }
}