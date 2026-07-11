using SSMS.Domain.Entities;
using SSMS.Domain.Repositories.Categories;
using SSMS.Infrustructure.DatabaseConfig;
using SSMS.Infrustructure.Repositories.Base;

namespace SSMS.Infrustructure.Repositories.Categories
{
    public class CategoryRepository(SSMSContext context) : Repository<Category>(context), ICategoryRepository
    {
    }
}
