namespace SDPhotosDownloader;

public class DirectoryScanner
{
    private const string FolderFormat = "yyyy_MM_dd";
    private const int MbDivider = 1024 * 1024;
    
    private readonly IConsoleWriter _consoleWriter;
    private readonly IFileSystem _fileSystem;
    
    public DirectoryScanner(IFileSystem fileSystem, IConsoleWriter consoleWriter)
    {
        _fileSystem = fileSystem;
        _consoleWriter = consoleWriter;
    }
    
    public IScanResult Scan(string rootPath, DateTime dateFrom)
    {
        var groups = new Dictionary<string, IList<FileEntry>>();
        var count = 0;
        var sizeMb = 0.0;
        RecursiveScan(rootPath, dateFrom, groups, ref count, ref sizeMb);
        return new ScanResult(groups, count, sizeMb);
    }
    
    public double GetDestinationFreeSpace(string path)
    {
        var di = new DriveInfo(path);
        double freeSpaceInBytes = di.AvailableFreeSpace;
        return freeSpaceInBytes / MbDivider;
    }
    
    private void RecursiveScan(string directoryPath, DateTime fromDate, Dictionary<string, IList<FileEntry>> groups, 
        ref int count, ref double sizeMb)
    {
        try
        {
            foreach (var filePath in _fileSystem.GetFiles(directoryPath))
            {
                var file = _fileSystem.GetFileEntry(filePath);
                if (file.CreationTime < fromDate) continue;
                var key = file.CreationTime.ToString(FolderFormat);
                if (!groups.ContainsKey(key)) groups[key] = new List<FileEntry>();
                groups[key].Add(file);
                count++;
                sizeMb += (double)file.Length / MbDivider;
            }

            foreach (var subdir in _fileSystem.GetDirectories(directoryPath))
            {
                RecursiveScan(subdir, fromDate, groups, ref count, ref sizeMb);
            }
        }
        catch (Exception ex)
        {
            _consoleWriter.WriteLine($"ERROR while traversing the directory: {ex.Message}");
        }
    }
}