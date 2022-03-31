using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Moq;
using System.Threading.Tasks;
using TechShowcase.Services;
using Xunit;

namespace TechShowcase.Tests;
public class ApplicationTests : AutoDataAttribute
{
    public ApplicationTests()
    {
    }

    [Fact]
    public async Task GivenAppStart_ThenReturnNiceGreeting()
    {
    }
}
