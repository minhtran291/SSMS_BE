using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Domain.Model
{
    public class ProductSeedModel
    {
        public string Name { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string Brand { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
