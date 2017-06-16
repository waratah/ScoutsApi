using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Scouts.Core.Interface;
using Scouts.Core.Model;
using Scouts.Core.Model.Entities;
using Scouts.Core.Model.Requests;

namespace Scouts.Controllers
{
    [Route("transaction")]
    public class TransactionController : Controller
    {
        ITransactionData _data;

        public TransactionController(ITransactionData data)
        {
            _data = data;
        }

        [HttpGet("{st}/{start}/{end}")]
        public IEnumerable<Transaction> Get(ScoutType st, DateTime start, DateTime end)
        {
            return _data.GetList(st,new DateRange { Start = start, End = end });
        }

        [HttpPost]
        public Transaction Post([FromBody]Transaction value)
        {
           return _data.Save(value);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException("Delete transaction not implemented");
        }
    }
}
