using System.Collections.Generic;
using System.Linq;
using Scouts.Core.Interface;
using Scouts.Core.Model;

namespace Scouts.Data
{
    public class ScoutRepository : IScoutData
    {
        ScoutContext _context;
        public ScoutRepository(ScoutContext context)
        {
            _context = context;
        }

        public Scout Get(int id)
        {
            return _context.Scouts.Find(id);
        }

        public IEnumerable<ScoutEmail> GetActiveEmails(ScoutType st)
        {
            return _context.Scouts
                .Where(x=>x.Section == st && x.Active)
                .Select(x=>new ScoutEmail {
                    Email = x.Email,
                    ScoutId = x.ScoutId,
            })
            .ToList();
        }

        public IEnumerable<ScoutSummary> GetList(ScoutType st, bool includeDeleted)
        {
            return _context.Scouts
                .Where(x => x.Section == st && (x.Active || includeDeleted))
                .Select(x => new ScoutSummary
                {
                    Active = x.Active,
                    Balance = x.Balance,
                    First = x.FirstName,
                    Last = x.LastName,
                    MemberNumber = x.MemberNumber,
                    ScoutId = x.ScoutId,
                    Section = x.Section,
                })
                .OrderBy(x=>x.First)
                .ThenBy(x => x.Last)
                .ToList();
        }

        public Scout GetNumber(string scoutNumber)
        {
            return _context.Scouts
                .Where(x => x.MemberNumber == scoutNumber )
                .FirstOrDefault();
        }

        public int Save(Scout scout)
        {
            var db = _context.Scouts
                .Where(x => x.ScoutId == scout.ScoutId)
                .FirstOrDefault();

            if (db == null)
            {
                _context.SetModified(scout);
                _context.SaveChanges();
                return scout.ScoutId;
            }
            db.Active = scout.Active ;
            db.FirstName = scout.FirstName;
            db.LastName= scout.LastName;
            db.MemberNumber = scout.MemberNumber;
            db.DOB = scout.DOB;
            db.Email = scout.Email;

            _context.SaveChanges();
            return db.ScoutId;
        }
    }
}
