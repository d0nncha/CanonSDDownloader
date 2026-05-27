namespace SDPhotosDownloader;

public interface IConsoleWriter
{
    void WriteLine(string? message = null);
    void Write(string? message);
    void WriteProgress(double percent);
}