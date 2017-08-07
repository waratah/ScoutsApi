using System.Linq;
using System.Collections.Generic;
using Moq;
using Scouts.Data;
using Xunit;
using Scouts.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Test.Scouts.Data
{
    public class ScoutRepositoryTest
    {
        ScoutRepository _repository;
        Mock<ScoutContext> _context;
        Mock<DbSet<Scout>> _scouts;
        List<Scout> _scoutsInput;

        [Fact]
        public void ScoutRepositoryGet()
        {
            Create();

            _repository = new ScoutRepository(_context.Object);
            _scouts.Setup(x => x.Find(45)).Returns(new Scout { Balance = 50, FirstName = "test", ScoutId = 45, Active = true });
            var result = _repository.Get(45);
            Assert.NotNull(result);
            Assert.Equal("test", result.FirstName);
        }

        [Fact]
        public void ScoutRepositorySave()
        {
            Create();

            var scout = new Scout { Balance = 50, FirstName = "test", ScoutId = 0, Active = true };
            _context.Setup(x => x.SetModified(It.IsAny<object>()));

            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository = new ScoutRepository(_context.Object);

            _repository.Save(scout);

            _context.Verify(x => x.SaveChanges(), Times.Once);
        }

        [Fact]
        public void ScoutRepositoryEmails()
        {
            Create();

            var emails = _repository.GetActiveEmails(ScoutType.Scout).ToList();

            Assert.Equal(2, emails.Count());
        }

        [Fact]
        public void ScoutRepositoryListActive()
        {
            Create();

            var scouts = _repository.GetList(ScoutType.Scout, false).ToList();

            Assert.Equal(2, scouts.Count());
        }

        [Fact]
        public void ScoutRepositoryListAll()
        {
            Create();

            var scouts = _repository.GetList(ScoutType.Scout, true).ToList();

            Assert.Equal(3, scouts.Count());
        }

        [Fact]
        public void ScoutRepositoryExtractScout()
        {
            Create();

            var scout = _repository.GetNumber("105468");

            Assert.Equal("test", scout.FirstName);
        }

        [Fact]
        public void ScoutRepositoryExtractScoutInvalid()
        {
            Create();

            var scout = _repository.GetNumber("205468");

            Assert.Null(scout);
        }

        void Create()
        {
            var config = new Mock<IConfiguration>();
            var section = new ConfigurationSection();
            config.Setup(x => x.GetSection(It.IsAny<string>())).Returns(section);

            _context = new Mock<ScoutContext>(config.Object);
            _scoutsInput = new List<Scout>
            {
                new Scout { Balance = 50, FirstName = "test", ScoutId = 45, Active = true, Email = "test1@test.com", Section = ScoutType.Scout, MemberNumber = "105468" },
                new Scout { Balance = 52, FirstName = "test 2", ScoutId = 2, Active = false, Email = "test2@test.com", Section = ScoutType.Scout, MemberNumber = "105467" },
                new Scout { Balance = 88, FirstName = "test cub", ScoutId = 99, Active = false, Email = "test3@test.com", Section = ScoutType.Cub, MemberNumber = "105466" },
                new Scout { Balance = 56, FirstName = "test 4", ScoutId = 48, Active = true, Email = "test4@test.com", Section = ScoutType.Scout, MemberNumber = "105465" }
            };
            _scouts = _scoutsInput.CreateDbSetMock();
            _context.Setup(x => x.Scouts).Returns(_scouts.Object);

            _repository = new ScoutRepository(_context.Object);
        }
    }
}
