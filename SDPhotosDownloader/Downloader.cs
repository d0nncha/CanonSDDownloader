namespace SDPhotosDownloader;

public static class Downloader
{
    private const string FolderFormat = "yyyy_MM_dd";

    private const int MbDivider = 1024 * 1024;

    private static readonly Dictionary<string, IList<FileInfo>> SourceFiles = new ();

    private static int _filesCounter = 0;

    private static double _fileSizeCounter = 0;
    
    public static void Start(string src, string dest, DateTime fromDate, bool isOverride)
    {
        Console.WriteLine($"Destination folder: {dest}");
        var destFreeSpace = GetDestinationFreeSpace(dest);
        Console.WriteLine($"Available free space: {destFreeSpace:N} Mb");
        
        Console.WriteLine($"File upload start date: {fromDate:D}");
        
        Console.WriteLine($"Source folder: {src}");
        Console.WriteLine("The starting of source scan...");
        _filesCounter = 0;
        _fileSizeCounter = 0.0;
        RecursiveDirectoryTraversal(src, fromDate);
        Console.WriteLine($"Number of source files: {_filesCounter}");
        Console.WriteLine($"The total size of the source files: {_fileSizeCounter:N} Mb");

        if (_fileSizeCounter >= destFreeSpace)
        {
            Console.WriteLine("Error: not enough disk space!");
            return;
        }
        
        Console.WriteLine("The starting of downloading...");
        Download(dest, SourceFiles, isOverride);
        Console.WriteLine("Download files finished!");
    }

    private static void RecursiveDirectoryTraversal(string directoryPath, DateTime fromDate)
    {
        try
        {
            var files = Directory.GetFiles(directoryPath);

            foreach (var file in files)
            {
                var fileInfo = new FileInfo(file);
                if (fileInfo.CreationTime < fromDate)
                {
                    continue;
                }

                var createDate = fileInfo.CreationTime.ToString(FolderFormat);
                if (!SourceFiles.ContainsKey(createDate))
                {
                    SourceFiles.Add(createDate, new List<FileInfo>());
                }
                SourceFiles[createDate].Add(fileInfo);
                _filesCounter++;
                _fileSizeCounter += (double)fileInfo.Length / MbDivider;
            }

            // Рекурсивно обходим поддиректории
            string[] subdirectories = Directory.GetDirectories(directoryPath);
            foreach (string subdirectory in subdirectories)
            {
                RecursiveDirectoryTraversal(subdirectory, fromDate);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при обходе директории: " + ex.Message);
        }
    }

    private static void Download(string destination, IDictionary<string, IList<FileInfo>> groupFiles, bool isOverride)
    {
        foreach (var group in groupFiles)
        {
            var destFolder = Path.Combine(destination, group.Key);
            if (!Path.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            foreach (var fileInfo in group.Value)
            {
                var destFileName = Path.Combine(destFolder, fileInfo.Name);
                File.Copy(fileInfo.FullName, destFileName, isOverride);
            }
        }
    }

    private static double GetDestinationFreeSpace(string path)
    {
        var di = new DriveInfo(path);
        double freeSpaceInBytes = di.AvailableFreeSpace;
        return freeSpaceInBytes / MbDivider;
    }
}