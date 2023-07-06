using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartwyre.DeveloperTest.Runner
{
    internal class ServiceHelper
    {
        private readonly IRebateService _rebateService;
        public ServiceHelper(IRebateService rebateService) 
        {
            _rebateService = rebateService;
        }

        public void Run(string[] args)
        {
            //var result = _rebateService.Calculate(new Types.CalculateRebateRequest());

            PaymentServiceTests paymentServiceTests = new PaymentServiceTests();

            paymentServiceTests.Test_AmountPerUom_Success();
            paymentServiceTests.Test_FixedRateRebate_Success();
            paymentServiceTests.Test_FixedCashAmount_Success();
        }
    }
}
