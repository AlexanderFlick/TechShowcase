using AutoFixture;
using AutoFixture.AutoMoq;

namespace PhotoAlbum.Tests;

public class TestBase
{
    public readonly Fixture _fixture = (Fixture)new Fixture().Customize(new AutoMoqCustomization());
}