namespace SDPhotosDownloader;

public interface IConsoleWriter
{
    /// <summary>
    /// Writes a message to the console, followed by a new line. If the message is null, only a new line is written.
    /// </summary>
    /// <param name="message">The message to write.</param>
    void WriteLine(string? message = null);
    
    /// <summary>
    /// Writes a message to the console without a newline. If the message is null, it writes nothing.
    /// </summary>
    /// <param name="message">The message to write.</param>
    void Write(string? message);
    
    /// <summary>
    /// Writes the progress percentage to the console.
    /// </summary>
    /// <param name="percent">The progress percentage.</param>
    void WriteProgress(double percent);
}