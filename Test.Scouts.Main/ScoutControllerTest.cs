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
    public class ScoutControllerTest
    {
        ScoutController _controller;
        Mock<IScoutData> _repository;

        [Fact]
        public void ScoutControllerGet()
        {
            Create();
            _repository.Setup(x => x.Get(1))
                .Returns(new Scout { Balance = 45, FirstName = "Fred", Active = true });

            var result = _controller.Get(1);
            Assert.Equal(45M, result.Balance);
            Assert.Equal("Fred", result.FirstName);
        }

        [Fact]
        public void ScoutControllerSave()
        {
            Create();
            var input = new Scout { Balance = 45, FirstName = "Fred", Active = true };
            _repository.Setup(x => x.Save(input))
                .Verifiable();

            _controller.Post(input);
            _repository.Verify(x=>x.Save(input));
        }

        public void Create()
        {
            _repository = new Mock<IScoutData>();
            _controller = new ScoutController(_repository.Object);
        }
    }
}
