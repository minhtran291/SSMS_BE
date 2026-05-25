using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Domain.Entities
{
    public class Size : BaseEntity
    {
        public int Id { get; set; }
        public int Value { get; set; }
        public virtual ICollection<ProductSizePrice> ProductSizePrices { get; set; } = [];
    }
}
