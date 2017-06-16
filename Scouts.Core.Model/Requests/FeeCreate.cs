using System;

namespace Scouts.Core.Model.Requests
{
    public class FeeCreate
    {
        public string Description { get; set; }
        public decimal Cost { get; set; }
    }
}
