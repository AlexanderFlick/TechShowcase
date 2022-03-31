using TechShowcase.Wrappers;

namespace TechShowcase.Services;

public interface IConsoleService
{
}
public class ConsoleService : IConsoleService
{
    private readonly IConsoleWrapper _console;

    public ConsoleService(IConsoleWrapper console)
    {
        _console = console;
    }

    public void Greeting()
    {
        throw new NotImplementedException();
    }
}
