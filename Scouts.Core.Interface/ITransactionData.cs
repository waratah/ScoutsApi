using System;
using System.Collections.Generic;
using Scouts.Core.Model;
using Scouts.Core.Model.Entities;
using Scouts.Core.Model.Requests;

namespace Scouts.Core.Interface
{
    public interface ITransactionData
    {
        IEnumerable<Transaction> GetList(ScoutType st, DateRange dates);
        IEnumerable<Transaction> GetScout(int scoutId);
        Transaction Save(Transaction transaction);
    }
}
