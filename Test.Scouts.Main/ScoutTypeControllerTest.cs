using Xunit;
using Scouts.Controllers;

namespace Test.Scouts.Main
{
    public class ScoutTypeControllerTest
    {
        ScoutTypeController _controller;

        [Fact]
        public void ScoutTypeControllerGet()
        {
            Create();

            var result = _controller.ScoutTypes();
            Assert.NotEmpty(result);
            Assert.Equal("Joey", result[0].Name);
            Assert.Equal(1, result[0].Value);
        }

        public void Create()
        {
            _controller = new ScoutTypeController();
        }
    }
}
