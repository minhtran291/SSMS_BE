using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace SSMS.Infrastructure.Seed
{
    public static class SeedHelpers
    {
        public static async Task<T> GetOrCreateAsync<T>(
            DbSet<T> dbset, 
            Expression<Func<T, bool>> predicate, 
            Func<T> createFunc)
            where T : class
        {
            var localPredicate = predicate.Compile();
            var entity = dbset.Local.FirstOrDefault(localPredicate)
                ?? await dbset.FirstOrDefaultAsync(predicate);

            if (entity == null)
            {
                entity = createFunc();
                await dbset.AddAsync(entity);
            }

            return entity;
        }
    }

    /*
    nhan vao dbset
    de truy cap vao local hay db
    roi dung firstordefault de duyet qua tung phan tu trong local hay trong db
    voi predicate duoc dinh nghia 
    thi func<T, bool> tuc la 1 ham nhan vao 1 object kieu T va tra ve true/false
    nguyen tac cua func thi type cuoi la kieu du lieu tra ve
    con nhung cai truoc la input, neu func chi co 1 tham so
    thi tuc la ham ko co input chi co kieu tra ve
    thi expression la bieu thuc cho truy van db
    nen predicat.Compile de chuyen expression thanh func de
    thuc thi tren local
    firstordefault se dua vao predicate tra ve true hay false
    true -> lay object day luon
    con false 
    thi dau tien se chay het local ma false -> null
    roi chay db van false het -> null
    roi sao do tao object moi roi return object do
    
    viec ma expression bao func thi chi don gian la func la ham chay
    thong thuong, con expression thi chi don gian la bien func thanh
    thanh mot mo ta co the doc duoc roi thanh chuyen thanh truy van
    sql trong db
     */
}
