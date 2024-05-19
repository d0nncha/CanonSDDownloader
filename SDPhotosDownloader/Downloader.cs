namespace SDPhotosDownloader;

public static class Downloader
{
    private const string folderFormat = "yyyy_MM_dd";

    private const int mbDevider = 1024 * 1024;

    private static readonly Dictionary<string, IList<FileInfo>> sourceFiles = new Dictionary<string, IList<FileInfo>>();

    private static int filesCounter = 0;

    private static double fileSizeCounter = 0;
    
    public static void Start(string src, string dest, DateTime date)
    {
        Console.WriteLine($"Destination folder: {dest}");
        var destFreeSpace = GetDestinationFreeSpace(dest);
        Console.WriteLine($"Available free space: {destFreeSpace:N} Mb");
        
        Console.WriteLine($"File upload start date: {date:D}");
        
        Console.WriteLine($"Source folder: {src}");
        Console.WriteLine("The starting of source scan...");
        filesCounter = 0;
        fileSizeCounter = 0.0;
        RecursiveDirectoryTraversal(src);
        Console.WriteLine($"Number of source files: {filesCounter}");
        Console.WriteLine($"The total size of the source files: {fileSizeCounter:N} Mb");

        if (fileSizeCounter >= destFreeSpace)
        {
            Console.WriteLine("Error: not enough disk space!");
            return;
        }
    }

    private static void RecursiveDirectoryTraversal(string directoryPath)
    {
        try
        {
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                var fileInfo = new FileInfo(file);
                var createDate = fileInfo.CreationTime.ToString(folderFormat);

                if (!sourceFiles.ContainsKey(createDate))
                {
                    sourceFiles.Add(createDate, new List<FileInfo>());
                }
                sourceFiles[createDate].Add(fileInfo);
                filesCounter++;
                fileSizeCounter += (double)fileInfo.Length / mbDevider;
            }

            // Рекурсивно обходим поддиректории
            string[] subdirectories = Directory.GetDirectories(directoryPath);
            foreach (string subdirectory in subdirectories)
            {
                RecursiveDirectoryTraversal(subdirectory);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при обходе директории: " + ex.Message);
        }
    }

    private static double GetDestinationFreeSpace(string path)
    {
        DriveInfo di = new DriveInfo(path);
        double freeSpaceInBytes = di.AvailableFreeSpace;
        return freeSpaceInBytes / mbDevider;
    }
}