using Xunit;
using AutoFixture;
using AutoFixture.AutoMoq;
using Moq;
using AutoFixture.Xunit2;
using TechShowcase.Wrappers;
using TechShowcase.Services;
using FluentAssertions;

namespace TechShowcase.Tests.ServiceTests;

public class ConsoleServiceTests : AutoDataAttribute
{
    private readonly ConsoleService _sut;
    private readonly Mock<IConsoleWrapper> _consoleMock;
    private readonly Fixture _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());

    public ConsoleServiceTests()
    {
        _consoleMock = _fixture.Freeze<Mock<IConsoleWrapper>>();
        _sut = _fixture.Create<ConsoleService>();
    }

    [Fact]
    public void GivenStartOfApplication_ThenReturnANiceGreeting()
    {
        _sut.Greeting();

        _consoleMock.Verify(c => c.Write("Welcome! Give me the number of the photo album that you want to view."), Times.Exactly(1));
    }

    [Fact]
    public void GivenInputFromUser_WhenInvalid_ReturnZero()
    {
        var invalidInput = _fixture.Create<string>();
        _consoleMock.Setup(c => c.Read()).Returns(invalidInput);

        var actual = _sut.GetAlbumIdFromInput();

        actual.Should().Be(0);
    }

    [Fact]
    public void GivenInputFromUser_WhenValid_ReturnAlbumId()
    {
        var validInput = _fixture.Create<int>();
        _consoleMock.Setup(c => c.Read()).Returns(validInput.ToString());

        var actual = _sut.GetAlbumIdFromInput();

        actual.Should().Be(validInput);
    }
}
