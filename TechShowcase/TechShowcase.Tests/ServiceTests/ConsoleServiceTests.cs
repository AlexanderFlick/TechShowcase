using Xunit;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using AutoFixture.Xunit2;
using TechShowcase.Wrappers;
using TechShowcase.Services;

namespace TechShowcase.Tests.ServiceTests;

public class ConsoleServiceTests : AutoDataAttribute
{
    private readonly ConsoleService _sut;
    private readonly Mock<IConsoleWrapper> _consoleMock;

    public ConsoleServiceTests()
    {
        var _fixture = new Fixture().Customize(new AutoMoqCustomization());
        _consoleMock = _fixture.Freeze<Mock<IConsoleWrapper>>();
        _sut = _fixture.Create<ConsoleService>();
    }

    [Fact]
    public void GivenStartOfApplication_ThenReturnANiceGreeting()
    {
        _sut.Greeting();

        _consoleMock.Verify(c => c.Write("Welcome! Give me the number of the photo album that you want to view."), Times.Exactly(1));
    }
}
