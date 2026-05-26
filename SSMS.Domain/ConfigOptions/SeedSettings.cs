using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSMS.Domain.ConfigOptions
{
    public class SeedSettings
    {
        public UserSeedConfig Admin { get; set; } = new();
        public UserSeedConfig Customer { get; set; } = new();
    }
}
