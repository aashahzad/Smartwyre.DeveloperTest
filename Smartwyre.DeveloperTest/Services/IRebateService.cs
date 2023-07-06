using Smartwyre.DeveloperTest.Types;
using System.Runtime.CompilerServices;

namespace Smartwyre.DeveloperTest.Services;

public interface IRebateService
{
    CalculateRebateResult Calculate(CalculateRebateRequest request);
}


