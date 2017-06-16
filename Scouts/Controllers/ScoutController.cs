using System;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Scouts.Core.Interface;
using Scouts.Core.Model;

namespace Scouts.Controllers
{
    [Route("scout")]
    public class ScoutController : Controller
    {
        IScoutData _data;

        public ScoutController(IScoutData data)
        {
            _data = data;
        }

        [HttpGet("{id}")]
        public Scout Get(int id)
        {
            return _data.Get(id);
        }

        [HttpPost]
        [DisableCors]
        public int Post([FromBody]Scout value)
        {
            return _data.Save(value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException("Delete scout not implemented");
        }
    }
}
