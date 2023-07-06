using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Services;
using System;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    private string productIdentifier = Guid.NewGuid().ToString();
    private string rebateIdentifier = Guid.NewGuid().ToString();

    [Fact]
    public void Test_FixedCashAmount_Success()
    {
        var rebateMock = new Mock<IRebateDataStore>();
        var productMock = new Mock<IProductDataStore>();
        rebateMock.Setup(x=> x.GetRebate(rebateIdentifier)).Returns(new Types.Rebate() 
                                                                    { 
                                                                        Incentive = new FixedCashAmount(), 
                                                                        Amount = 10 
                                                                    });
        productMock.Setup(x=> x.GetProduct(productIdentifier)).Returns(new Types.Product() 
                                                                    { 
                                                                        SupportedIncentives = new FixedCashAmount() 
                                                                    });

        var rebateService = new RebateService(productMock.Object, rebateMock.Object);
        var response = rebateService.Calculate(new Types.CalculateRebateRequest() { ProductIdentifier = productIdentifier, RebateIdentifier = rebateIdentifier });
        Assert.Equal(10, response.RebateAmount);
    }

    [Fact]
    public void Test_FixedRateRebate_Success()
    {
        var rebateMock = new Mock<IRebateDataStore>();
        var productMock = new Mock<IProductDataStore>();
        rebateMock.Setup(x => x.GetRebate(rebateIdentifier)).Returns(new Types.Rebate() 
                                                                            { 
                                                                                Incentive = new FixedRateRebate(), 
                                                                                Percentage = 3,
                                                                            });
        productMock.Setup(x => x.GetProduct(productIdentifier)).Returns(new Types.Product() 
                                                                            { 
                                                                                SupportedIncentives = new FixedRateRebate(),
                                                                                Price = 3
                                                                            });

        var rebateService = new RebateService(productMock.Object, rebateMock.Object);
        var response = rebateService.Calculate(new Types.CalculateRebateRequest() 
                                                        { 
                                                            ProductIdentifier = productIdentifier, 
                                                            RebateIdentifier = rebateIdentifier,
                                                            Volume = 3
                                                        });
        Assert.Equal(27, response.RebateAmount);
    }

    [Fact]
    public void Test_AmountPerUom_Success()
    {
        var rebateMock = new Mock<IRebateDataStore>();
        var productMock = new Mock<IProductDataStore>();
        rebateMock.Setup(x => x.GetRebate(rebateIdentifier)).Returns(new Types.Rebate()
        {
            Incentive = new AmountPerUom(),
        });
        productMock.Setup(x => x.GetProduct(productIdentifier)).Returns(new Types.Product()
        {
            SupportedIncentives = new AmountPerUom(),
            Price = 3
        });

        var rebateService = new RebateService(productMock.Object, rebateMock.Object);
        var response = rebateService.Calculate(new Types.CalculateRebateRequest()
        {
            ProductIdentifier = productIdentifier,
            RebateIdentifier = rebateIdentifier,
            Volume = 3
        });
        Assert.Equal(9, response.RebateAmount);
    }

}
