using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Moq;
using Scouts.Core.Interface;
using Scouts.Controllers;
using Scouts.Core.Model;
using Scouts.Core.Model.Entities;
using Scouts.Core.Model.Requests;

namespace Test.Scouts.Main
{
    public class TransactionControllerTest
    {
        TransactionController _controller;
        Mock<ITransactionData> _repository;

        [Fact]
        public void TransactionControllerGet()
        {
            var range = new DateRange { Start = new DateTime(2012, 2, 1), End = new DateTime(2012, 2, 28) };
            Create();
            _repository.Setup(x => x.GetList(ScoutType.Scout, It.IsAny<DateRange>()))
                .Returns(new List<Transaction> {
                    new Transaction { Amount = 50, ReceiptId = "2", TransactionDate = new DateTime(2012,2,13) },
                });

            var result = _controller.Get(ScoutType.Scout, range.Start, range.End).ToList();
            Assert.NotEmpty(result);
            Assert.Equal(1, result.Count);
            Assert.Equal(50M, result[0].Amount);
            Assert.Equal("2", result[0].ReceiptId);
        }

        [Fact]
        public void TransactionControllerPost()
        {
            var input = new Transaction { Amount = 50, ReceiptId = "2" };

            Create();
            _repository.Setup(x => x.Save(input))
                .Returns(input);

            var result = _controller.Post(input);
            Assert.NotNull(result);
            Assert.Equal(50M, result.Amount);
            Assert.Equal("2", result.ReceiptId);
        }

        public void Create()
        {
            _repository = new Mock<ITransactionData>();
            _controller = new TransactionController(_repository.Object);
        }
    }
}
