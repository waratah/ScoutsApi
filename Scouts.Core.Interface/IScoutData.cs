using System.Collections.Generic;
using Scouts.Core.Model;

namespace Scouts.Core.Interface
{
    public interface IScoutData
    {
        Scout Get(int id);
        Scout GetNumber(string scoutNumber);
        IEnumerable<ScoutSummary> GetList(ScoutType st, bool includeDeleted);
        IEnumerable<ScoutEmail> GetActiveEmails(ScoutType st);
        int Save(Scout scout);
    }
}
