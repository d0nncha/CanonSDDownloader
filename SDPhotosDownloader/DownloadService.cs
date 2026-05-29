namespace SDPhotosDownloader;

public class DownloadService
{
    private readonly IDownloadOptions _options;
    private readonly IConsoleWriter _consoleWriter;
    private readonly IFileSystem _fileSystem;
    
    public DownloadService(IDownloadOptions options, IFileSystem fileSystem, IConsoleWriter consoleWriter)
    {
        _options = options;
        _fileSystem = fileSystem;
        _consoleWriter = consoleWriter;
    }
    
    public int Start()
    {
        _consoleWriter.WriteLine($"Destination folder: {_options.DestPath}");
        _consoleWriter.WriteLine($"File upload start date: {_options.DateFrom:D}");
        _consoleWriter.WriteLine($"Root folder: {_options.SrcPath}");
        _consoleWriter.WriteLine("The starting of source scan...");
        
        var scanner = new DirectoryScanner(_fileSystem, _consoleWriter);
        var scanResult = scanner.Scan(_options.SrcPath, _options.DateFrom);
        _consoleWriter.WriteLine($"Number of source files: {scanResult.FilesCount}");
        
        var destFreeSpace = scanner.GetDestinationFreeSpace(_options.DestPath);
        _consoleWriter.WriteLine($"Available free space: {destFreeSpace:N} Mb");
        _consoleWriter.WriteLine($"The total size of the source files: {scanResult.TotalSizeMb:N} Mb");

        if (scanResult.TotalSizeMb >= destFreeSpace)
        {
            _consoleWriter.WriteLine("Error: not enough disk space!");
            return 0;
        }
        
        _consoleWriter.WriteLine("The starting of downloading...");
        var downloadResult = Download(_options.DestPath, scanResult, _options.IsOverwrite);
        _consoleWriter.WriteLine("Download files finished!");
        _consoleWriter.WriteLine($"- {downloadResult.TotalProcessed} file(s) were processed");
        _consoleWriter.WriteLine($"- {downloadResult.TotalDownloaded} file(s) were downloaded");
        _consoleWriter.WriteLine($"- {downloadResult.Skipped} file(s) were skipped");
        _consoleWriter.WriteLine($"- {downloadResult.Overwritten} file(s) were overwritten");

        return 1;
    }
    
    private DownloadResult Download(string destination, IScanResult scanResult, bool isOverwrite)
    {
        var result = new DownloadResult();
        _consoleWriter.Write("0%");
        foreach (var group in scanResult.Groups)
        {
            var destFolder = Path.Combine(destination, group.Key);
            if (!Path.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            foreach (var fileInfo in group.Value)
            {
                var fileNameBuilder = $"{fileInfo.CreationTime:yyMMddHHmmss}_{fileInfo.Name}";
                var destFileName = Path.Combine(destFolder, fileNameBuilder);
                result.TotalProcessed++;
                if (File.Exists(destFileName))
                {
                    if (isOverwrite)
                    {
                        result.Overwritten++;
                    }
                    else
                    {
                        result.Skipped++;
                        continue;
                    }
                }
                File.Copy(fileInfo.FullName, destFileName, isOverwrite);
                result.TotalDownloaded++;
            }

            var donePercent = (double)result.TotalProcessed / scanResult.FilesCount;
            _consoleWriter.WriteProgress(donePercent);
        }
        _consoleWriter.WriteLine();
        return result;
    }
}