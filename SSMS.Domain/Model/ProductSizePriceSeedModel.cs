using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Domain.Model
{
    public class ProductSizePriceSeedModel
    {
        public string Product { get; set; } = null!;
        public int Size { get; set; }
        public decimal Price { get; set; }
    }
}
