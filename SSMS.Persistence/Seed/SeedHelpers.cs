using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Persistence.Seed
{
    public static class SeedHelpers
    {
        public static async Task<T> GetOrCreateAsync<T>(
            DbSet<T> dbset, 
            Expression<Func<T, bool>> predicate, 
            Func<T> createFunc)
            where T : class
        {
            var entity = await dbset.FirstOrDefaultAsync(predicate);

            if (entity == null)
            {
                entity = createFunc();
                await dbset.AddAsync(entity);
            }

            return entity;
        }
    }
}
