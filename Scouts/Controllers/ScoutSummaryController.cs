using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Scouts.Core.Interface;
using Scouts.Core.Model;

namespace Scouts.Controllers
{
    [Route("ScoutSummary")]
    public class ScoutSummaryController : Controller
    {
        IScoutData _data;

        public ScoutSummaryController(IScoutData data)
        {
            _data = data;
        }

        [HttpGet("{st}")]
        public IEnumerable<ScoutSummary> Get(ScoutType st)
        {
            return _data.GetList( st, false);
        }

        [HttpGet("emails/{st}")]
        public IEnumerable<ScoutEmail> Emails(ScoutType st)
        {
            var result = _data.GetActiveEmails(st);
            return result;
        }
    }
}
