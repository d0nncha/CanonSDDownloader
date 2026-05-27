namespace SDPhotosDownloader;

public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string? message = null) => Console.WriteLine(message);
    
    public void Write(string? message) => Console.Write(message);
    
    public void WriteProgress(double percent) => Console.Write($"\r{percent:P}");
}