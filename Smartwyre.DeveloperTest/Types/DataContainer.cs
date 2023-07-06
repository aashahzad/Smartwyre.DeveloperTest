using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Types
{
    public class DataContainer
    {
        public Rebate Rebate { get; set; } 
        public Product Product { get; set; }
        public CalculateRebateRequest CalculateRebateRequest { get; set;}
    }


}
