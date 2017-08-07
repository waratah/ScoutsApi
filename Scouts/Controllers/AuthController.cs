using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Scouts.Core.Interface;
using Scouts.Core.Model;

namespace Scouts.Controllers
{
    [Route("auth")]
    public class AuthController : Controller
    {
        IFeeData _data;

        public AuthController(IFeeData data)
        { 
            _data = data;
        }

        [HttpGet("")]
        public IEnumerable<Fee> Get()
        {
            try
            {

                return _data.Get();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        [HttpPost]
        public void Post([FromBody]IEnumerable<Fee> value)
        {
            _data.SaveAll(value);
        }


        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            throw new NotImplementedException("Delete fee not implemented");
        }
    }
}
