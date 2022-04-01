using AutoFixture.Xunit2;
using System.Threading.Tasks;
using Xunit;

namespace PhotoAlbum.Tests;
public class ApplicationTests : AutoDataAttribute
{
    public ApplicationTests()
    {
    }

    [Fact]
    public async Task GivenAppStart_CheckServiceCalls()
    {
    }
}
