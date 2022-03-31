namespace TechShowcase.Wrappers;

public interface IConsoleWrapper
{
    void Write(string message);
    string Read();
}
public class ConsoleWrapper : IConsoleWrapper
{
    public void Write(string message) => Console.WriteLine(message);

    public string Read() => Console.ReadLine();
}
