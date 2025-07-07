using Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.SeedData
{
    public static class SeedExtention
    {
        public async static Task SeedState(this SchoolDbContext db)
        {
            if(!await db.states.AnyAsync())
            {
                var states = new List<State>();
                using (var sr = new StreamReader("SeedData/State.csv"))
                {
                    var fulltext = await sr.ReadToEndAsync();
                    var rows = fulltext.Split('\n').Skip(1);
                    states.AddRange(rows.Select(row => row.Split(','))
                        .Select(column => new State
                        {
                            Id = Guid.NewGuid(),
                            Name = column[0].Trim(),
                            Code = column[1].Trim(),
                            LastModefiedBy = "system"
                        }));
                }
                db.AddRange(states);
                await db.TrySaveChangesAsync();
            }
        }
        public async static Task SeedLocalGovernment(this SchoolDbContext db)
        {
            var states = await db.states.ToListAsync();
            if(!await db.localGovnments.AnyAsync())
            {
                var localGovernments = new List<LocalGovnment>();
                using(var sr = new StreamReader("SeedData/LGA.csv"))
                {
                    var fulltext = await sr.ReadToEndAsync();
                    var rows = fulltext.Split('\n').Skip(1);
                    localGovernments.AddRange(rows.Select(row => row.Split(','))
                        .Select(column => new LocalGovnment
                        {
                            Id = Guid.NewGuid(),
                            Name = column[0].Trim(),
                            StateId = states.First(x => x.Code == column[1].Trim()).Id,
                            LastModefiedBy = "system"
                        }));
                }
                db.AddRange(localGovernments);
                await db.TrySaveChangesAsync();
            }
        }
    }
}
