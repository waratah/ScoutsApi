﻿using System.Collections.Generic;
using Scouts.Core.Model;

namespace Scouts.Core.Interface
{
    public interface ITermFeesData
    {
        IEnumerable<Fee> Get();
        void SaveAll(IEnumerable<Fee> fees);
        int Save(Fee fee);
    }
}
