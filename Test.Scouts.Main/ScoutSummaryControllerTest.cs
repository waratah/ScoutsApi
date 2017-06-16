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
    public class ScoutSummaryControllerTest
    {
        ScoutSummaryController _controller;
        Mock<IScoutData> _repository;

        [Fact]
        public void ScoutSummaryControllerGet()
        {
            Create();
            _repository.Setup(x => x.GetList(ScoutType.Scout, false))
                .Returns(new List<ScoutSummary> { new ScoutSummary { Balance = 45, First = "Fred", Active = true } });

            var result = _controller.Get(ScoutType.Scout).ToList();
            Assert.NotEmpty(result);
            Assert.Equal(45M, result[0].Balance);
            Assert.Equal("Fred", result[0].First);
        }

        [Fact]
        public void ScoutSummaryControllerEmails()
        {
            Create();
            var input = new Scout { Balance = 45, FirstName = "Fred", Active = true };
            _repository.Setup(x => x.GetActiveEmails(ScoutType.Scout))
                .Returns(new List<ScoutEmail> { new ScoutEmail { Email = "test@test.com", ScoutId = 1 } });

            var result = _controller.Emails(ScoutType.Scout).ToList();
            Assert.NotEmpty(result);
            Assert.Equal(1, result[0].ScoutId);
            Assert.Equal("test@test.com", result[0].Email);
        }

        public void Create()
        {
            _repository = new Mock<IScoutData>();
            _controller = new ScoutSummaryController(_repository.Object);
        }
    }
}
