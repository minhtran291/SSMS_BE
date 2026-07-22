using SSMS.Domain.Entities;
using SSMS.Application.Repositories.Categories;
using SSMS.Infrastructure.DatabaseConfig;
using SSMS.Infrastructure.Repositories.Base;

namespace SSMS.Infrastructure.Repositories.Categories
{
    public class CategoryRepository(SSMSContext context) : Repository<Category>(context), ICategoryRepository
    {
    }
}
