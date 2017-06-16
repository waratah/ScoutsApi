using System;
using System.Linq;
using System.Collections.Generic;
using Moq;
using Scouts.Data;
using Xunit;
using Scouts.Core.Model;
using Scouts.Core.Model.Entities;
using Scouts.Core.Model.Requests;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Test.Scouts.Data
{
    public class AccountRepositoryTest
    {
        AccountRepository _repository;
        Mock<AccountContext> _context;

        Mock<DbSet<Fee>> _fees;
        List<Fee> _feesInput;

        Mock<DbSet<Transaction>> _transaction;
        List<Transaction> _transactionInput;


        [Fact]
        public void AccountRepositoryGet()
        {
            Create();

            _feesInput.Add(new Fee { CurrentCost = 50, Description = "test", FeeId = 2, IsActive = true });
            _feesInput.Add(new Fee { CurrentCost = 52, Description = "test 2", FeeId = 2, IsActive = false });
            _context.Setup(x => x.Fees).Returns(_fees.Object);

            _repository = new AccountRepository(_context.Object);

            var result = _repository.Get();
            Assert.NotEmpty(result);
        }

        [Fact]
        public void AccountRepositorySaveAll()
        {
            Create();

            _feesInput.Add(new Fee { CurrentCost = 50, Description = "test", FeeId = 2, IsActive = true });
            _feesInput.Add(new Fee { CurrentCost = 52, Description = "test 2", FeeId = 2, IsActive = false });
            _context.Setup(x => x.Fees).Returns(_fees.Object);
            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository = new AccountRepository(_context.Object);

            _repository.SaveAll(_feesInput);

            _context.Verify(x => x.SaveChanges(), Times.Exactly(2));
        }

        [Fact]
        public void AccountRepositoryGetTransactionDateRange()
        {
            Create();

            _context.Setup(x => x.Transactions).Returns(_transaction.Object);
            TransactionBaseline();

            _repository = new AccountRepository(_context.Object);

            var transactions =  _repository.GetList(ScoutType.Unknown, new DateRange {Start = new DateTime(2012,2,1), End = new DateTime(2012,2,28) }).ToList();

            Assert.Equal(1, transactions.Count);
        }

        [Fact]
        public void AccountRepositoryGetTransactionDateRangeMissingData()
        {
            Create();

            _context.Setup(x => x.Transactions).Returns(_transaction.Object);
            TransactionBaseline();

            _repository = new AccountRepository(_context.Object);

            var transactions = _repository.GetList(ScoutType.Unknown, new DateRange { Start = new DateTime(2010, 2, 1), End = new DateTime(2010, 2, 28) }).ToList();

            Assert.Equal(0, transactions.Count);
        }

        [Fact]
        public void AccountRepositoryGetTransactionDateRangeFilterCub()
        {
            Create();

            _context.Setup(x => x.Transactions).Returns(_transaction.Object);
            TransactionBaseline();

            _repository = new AccountRepository(_context.Object);

            var transactions = _repository.GetList(ScoutType.Cub, new DateRange { Start = new DateTime(2010, 2, 1), End = new DateTime(2017, 2, 28) }).ToList();

            Assert.Equal(2, transactions.Count);
        }

        [Fact]
        public void AccountRepositoryGetTransactionDateRangeFilterScout()
        {
            Create();

            _context.Setup(x => x.Transactions).Returns(_transaction.Object);
            TransactionBaseline();

            _repository = new AccountRepository(_context.Object);

            var transactions = _repository.GetList(ScoutType.Scout, new DateRange { Start = new DateTime(2010, 2, 1), End = new DateTime(2017, 2, 28) }).ToList();

            Assert.Equal(1, transactions.Count);
        }

        [Fact]
        public void AccountRepositorySaveTransaction()
        {
            Create();

            _context.Setup(x => x.Transactions).Returns(_transaction.Object);
            _context.Setup(x => x.SaveChanges()).Verifiable();

            _repository = new AccountRepository(_context.Object);

            var transaction = new Transaction { ActivityId = 1, Amount = 50, BookNo =20, DiscountId = 1,  FeeId = 10, ReceiptId = "201", ScoutId = 1 };

            _repository.Save(transaction);

            _context.Verify(x => x.SaveChanges(), Times.Once);
        }

        public void TransactionBaseline()
        {
            var cub = new Scout { ScoutId = 1, Section = ScoutType.Cub };
            var scout = new Scout { ScoutId = 2, Section = ScoutType.Scout };
            _transactionInput.Add(new Transaction { ActivityId = 1, Amount = 50, BookNo = 20, DiscountId = 1, FeeId = 10, ReceiptId = "201", ScoutId = 1, TransactionId = 1, TransactionDate = new DateTime(2012, 2, 13), Scout = cub });
            _transactionInput.Add(new Transaction { ActivityId = 2, Amount = 50, BookNo = 20, DiscountId = 1, FeeId = 10, ReceiptId = "202", ScoutId = 1, TransactionId = 2, TransactionDate = new DateTime(2012, 3, 13), Scout = cub });
            _transactionInput.Add(new Transaction { ActivityId = 1, Amount = 50, BookNo = 20, DiscountId = 1, FeeId = 10, ReceiptId = "203", ScoutId = 2, TransactionId = 3, TransactionDate = new DateTime(2012, 4, 13), Scout = scout });

        }

        void Create()
        {
            var config = new Mock<IConfiguration>();
            var section = new ConfigurationSection();
            config.Setup(x => x.GetSection(It.IsAny<string>())).Returns(section);

            _feesInput = new List<Fee>();
            _fees = _feesInput.CreateDbSetMock();

            _transactionInput = new List<Transaction>();
            _transaction = _transactionInput.CreateDbSetMock();

            _context = new Mock<AccountContext>(config.Object);
            _repository = new AccountRepository(_context.Object);
        }
    }
}
