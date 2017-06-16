using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Scouts.Core.Interface;
using Scouts.Core.Model;
using Scouts.Core.Model.Entities;
using Scouts.Core.Model.Requests;

namespace Scouts.Data
{
    public class AccountRepository : IFeeData, ITransactionData
    {
        AccountContext _context;
        public AccountRepository(AccountContext context)
        {
            _context = context;
        }

        #region Fee data

        public IEnumerable<Fee> Get()
        {
            return _context.Fees
                .ToList();
        }


        public void SaveAll(IEnumerable<Fee> fees)
        {
            foreach( var fee in fees)
            {
                Save(fee);
            }
        }

        public int Save(Fee fee)
        {
            var db = _context.Fees
                .Where(x => x.FeeId == fee.FeeId)
                .FirstOrDefault();

            if (db == null)
            {
                _context.Entry(fee).State = EntityState.Added;
                _context.SaveChanges();
                return fee.FeeId;
            }
            db.IsActive = fee.IsActive ;
            db.Description = fee.Description;
            db.CurrentCost= fee.CurrentCost;

            _context.SaveChanges();
            return db.FeeId;
        }

        #endregion

        #region Scout Data

        public IEnumerable<Transaction> GetList(ScoutType st, DateRange dates)
        {
            var query = _context.Transactions
                .Where(x => x.TransactionDate >= dates.Start && x.TransactionDate <= dates.End);
            if( st != ScoutType.Unknown)
            {
                query = query.Where(x => x.Scout.Section == st);
            }

            return query.ToList();
        }

        public IEnumerable<Transaction> GetScout(int scoutId)
        {
            return _context.Transactions
                .Where(x => x.ScoutId == scoutId)
                .ToList();
        }

        public Transaction Save(Transaction transaction)
        {
            var db = _context.Transactions
                .Where(x => x.TransactionId == transaction.TransactionId)
                .FirstOrDefault();

            if (db == null)
            {
                _context.SetModified(transaction);
                _context.SaveChanges();
                return transaction;
            }

            db.DiscountId= transaction.DiscountId;
            db.ActivityId = transaction.ActivityId;
            db.Amount= transaction.Amount;
            db.BookNo = transaction.BookNo;
            db.FeeId = transaction.FeeId;
            db.ReceiptId = transaction.ReceiptId;
            db.ScoutId = transaction.ScoutId;
            _context.SaveChanges();
            return db;
        }

        #endregion

    }
}
