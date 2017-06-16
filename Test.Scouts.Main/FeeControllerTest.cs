using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Scouts.Core.Interface;
using Scouts.Controllers;
using Scouts.Core.Model;

namespace Test.Scouts.Main
{
    public class FeeControllerTest
    {
        FeesController _controller;
        Mock<IFeeData> _repository;

        [Fact]
        public void FeeControllerGet()
        {
            Create();
            _repository.Setup(x => x.Get())
                .Returns(new List<Fee> { new Fee { CurrentCost = 45, Description = "Test", IsActive = true } });

            var result = _controller.Get().ToList();
            Assert.NotEmpty(result);
            Assert.Equal(45M, result[0].CurrentCost);
        }

        [Fact]
        public void FeeControllerSave()
        {
            Create();
            var input = new List<Fee> { new Fee { CurrentCost = 45, Description = "Test", IsActive = true } };
            _repository.Setup(x => x.SaveAll(input))
                .Verifiable();

            _controller.Post(input);
            _repository.Verify(x=>x.SaveAll(input));
        }

        public void Create()
        {
            _repository = new Mock<IFeeData>();
            _controller = new FeesController(_repository.Object);
        }
    }
}
