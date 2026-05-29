using SSMS.Persistence.DatabaseConfig;
using SSMS.Persistence.Repositories.Base;

namespace SSMS.Persistence.Repositories.Category
{
    public class CategoryRepository(SSMSContext context) : Repository<Domain.Entities.Category>(context), ICategoryRepository
    {
    }
}
