using Microsoft.Extensions.Hosting;
using TechShowcase.Services;

namespace TechShowcase;
public class Application : IHostedService
{
    // give a nice greeting :)

    // take user input and retreive information

    //display information

    //ask if finished or if you want to view more items
    private readonly IConsoleService _console;

    public Application(IConsoleService console)
    {
        _console = console;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
