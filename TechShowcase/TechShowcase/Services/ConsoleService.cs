using TechShowcase.Wrappers;

namespace TechShowcase.Services;

public interface IConsoleService
{
    void Greeting();
    void GetUserInput();
}
public class ConsoleService : IConsoleService
{
    private readonly IConsoleWrapper _console;

    public ConsoleService(IConsoleWrapper console)
    {
        _console = console;
    }

    public void Greeting() => _console.Write("Welcome! Give me the number of the photo album that you want to view.");

    public void GetUserInput()
    {
        throw new NotImplementedException();
    }
}
