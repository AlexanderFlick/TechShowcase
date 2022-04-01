using AutoFixture;
using FluentAssertions;
using PhotoAlbum.Data.Extensions;
using PhotoAlbum.Data.Models;
using Xunit;

namespace PhotoAlbum.Tests.ExtensionTests;

public class PhotoExtensionsTests : TestBase
{
    private readonly Photo _sut;

    public PhotoExtensionsTests()
    {
        _sut = _fixture.Create<Photo>();
    }

    [Fact]
    public void GivenPhoto_WhenWritingToConsole_ThenReturnFormattedInfo()
    {
        var actual = _sut.Info();

        actual.Should().Be($"--------------------\nPhoto Id: {_sut.Id}\nPhoto Title: {_sut.Title}\n--------------------");
    }
}