using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Scouts.Core.Model;

namespace Scouts.Controllers
{
    [Route("ScoutType")]
    public class ScoutTypeController : Controller
    {
        [HttpGet]
        public List<EnumElement> ScoutTypes()
        {
            var list = new List<EnumElement>();
            foreach (ScoutType suit in Enum.GetValues(typeof(ScoutType)))
            {
                if (suit != ScoutType.Unknown)
                {
                    list.Add(new EnumElement { Value = (int)suit, Name = suit.ToString() });
                }
            }
            return list;
        }

    }
}
