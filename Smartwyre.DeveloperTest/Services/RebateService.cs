using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Types;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    /*
     * 1- use built in ioc container to resolve dependencies.
     * 2- inject dependenciess through constrcutor
     * 3- provide further support to add up multiple types of rebate calculation (via an abstract class/interface with one calculate method inherted by all types)
     * 4- write test methods to cover all possible sccenarios with moc and nunit/xunit.
     */
    private readonly IProductDataStore productDataStore;
    private readonly IRebateDataStore rebateDataStore;
    public RebateService(IProductDataStore productDataStore, IRebateDataStore rebateDataStore) 
    {
        this.productDataStore = productDataStore;
        this.rebateDataStore = rebateDataStore;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var result = new CalculateRebateResult();
        var paramsContainer = new DataContainer();

        paramsContainer.CalculateRebateRequest = request;

        paramsContainer.Rebate = rebateDataStore.GetRebate(request.RebateIdentifier);
        paramsContainer.Product = productDataStore.GetProduct(request.ProductIdentifier);


        var rebateAmount = 0m;

        if (paramsContainer.Rebate != null && paramsContainer.Product != null)
        {
            if (paramsContainer.Rebate.Incentive != null &&
                paramsContainer.Product.SupportedIncentives != null &&
                paramsContainer.Rebate.Incentive.GetType().Equals(paramsContainer.Product.SupportedIncentives.GetType()))
            {
                result = paramsContainer.Rebate.Incentive.Calculate(paramsContainer, rebateAmount);
                rebateAmount = result.RebateAmount;
            }
        }
        if (result.Success)
        {
            //var storeRebateDataStore = new RebateDataStore();
            rebateDataStore.StoreCalculationResult(paramsContainer.Rebate, rebateAmount);
        }

        return result;


        //switch (rebate.Incentive)
        //{
        //    case IncentiveType.FixedCashAmount:
        //        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
        //        {
        //            result.Success = false;
        //        }
        //        else if (rebate.Amount == 0)
        //        {
        //            result.Success = false;
        //        }
        //        else
        //        {
        //            rebateAmount = rebate.Amount;
        //            result.Success = true;
        //        }
        //        break;

        //    case IncentiveType.FixedRateRebate:
        //        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
        //        {
        //            result.Success = false;
        //        }
        //        else if (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0)
        //        {
        //            result.Success = false;
        //        }
        //        else
        //        {
        //            rebateAmount += product.Price * rebate.Percentage * request.Volume;
        //            result.Success = true;
        //        }
        //        break;

        //    case IncentiveType.AmountPerUom:
        //        if (!product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
        //        {
        //            result.Success = false;
        //        }
        //        else if (rebate.Amount == 0 || request.Volume == 0)
        //        {
        //            result.Success = false;
        //        }
        //        else
        //        {
        //            rebateAmount += rebate.Amount * request.Volume;
        //            result.Success = true;
        //        }
        //        break;
        //}
        //}


    }
}

public abstract class IncentiveType 
{
    public abstract CalculateRebateResult Calculate(DataContainer inputParams, decimal rebateAmount = 0);
    protected abstract void SetInputValues(DataContainer inputParams);
}
public class FixedCashAmount : IncentiveType
{
    public decimal Amount { get; set; }

    protected override void SetInputValues(DataContainer inputParams)
    {
        this.Amount = inputParams.Rebate.Amount;
    }
    public override CalculateRebateResult Calculate(DataContainer inputParams, decimal rebateAmount = 0)
    {
        this.SetInputValues(inputParams);
        var response = new CalculateRebateResult();
        if (Amount == 0)
        {
            return response;
        }
        response.RebateAmount = Amount;
        response.Success = true;
        return response;
    }
}
public class FixedRateRebate : IncentiveType
{
    public decimal Percentage { get; set; }
    public decimal Price { get; set; }
    public decimal Volume { get; set; }

    protected override void SetInputValues(DataContainer inputParams)
    {
        Price = inputParams.Product.Price;
        Percentage = inputParams.Rebate.Percentage;
        Volume = inputParams.CalculateRebateRequest.Volume;
    }

    public override CalculateRebateResult Calculate(DataContainer inputParams, decimal rebateAmount = 0)
    {
        this.SetInputValues(inputParams);
        var response = new CalculateRebateResult();
        if (Percentage == 0 || Price == 0 || Volume == 0)
        {
            return response;
        }
        rebateAmount += Percentage * Price * Volume;
        response.RebateAmount = rebateAmount;
        response.Success = true;
        return response;
    }
}
public class AmountPerUom : IncentiveType
{
    public decimal Amount { get; set; }
    public decimal Volume { get; set; }

    protected override void SetInputValues(DataContainer inputParams)
    {
        Amount = inputParams.Product.Price;
        Volume = inputParams.CalculateRebateRequest.Volume;
    }

    public override CalculateRebateResult Calculate(DataContainer inputParams, decimal rebateAmount = 0)
    {
        this.SetInputValues(inputParams);
        var response = new CalculateRebateResult();
        if (Amount == 0 || Volume == 0)
        {
            return response;
        }

        rebateAmount += Amount * Volume;
        response.RebateAmount = rebateAmount;
        response.Success = true;
        return response;

    }
}




