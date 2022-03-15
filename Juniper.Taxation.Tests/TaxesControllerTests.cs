using System.Threading.Tasks;
using Juniper.Taxation.Controllers;
using Juniper.Taxation.Core.Application.Interfaces;
using Juniper.Taxation.Core.Application.Models;
using Juniper.Taxation.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;



namespace Juniper.Taxation.Tests
{
    
    public class TaxesControllerTests
    {
        private readonly Mock<ILogger<TaxesController>> _logger;
        private readonly Mock<ITaxCalculationService> _taxProvider;
        private readonly TaxesController _taxController;

        public TaxesControllerTests()
        {
            _logger=new Mock<ILogger<TaxesController>>();
            _taxProvider=new Mock<ITaxCalculationService>();
            _taxController = new TaxesController(_logger.Object, _taxProvider.Object);
        }

        [Fact]
        public async void GetTaxRateByLocaiton_GivenValidInput_Expect_success()
        {
            //setup
            var mockResponse = new GetTaxRateByLocationResponse
            {
                Rate = new TaxRate
                {
                    City = "SomeCity",
                    CityRate = "1.5",
                    CombinedRate = "10",
                    CombinedDistrictRate = "1",
                    County = "Hillborough",
                    CountyRate = "1.5",
                    FreightTaxable = false,
                    State = "FL",
                    StateRate = "6",
                    Zip = "33647"
                }
            };


            _taxProvider.Setup(m => m.GetTaxRateByLocation(It.IsAny<TaxByLocationQuery>()))
                .Returns(Task.FromResult(mockResponse)).Verifiable();

            var actualResponse =
                await _taxController.GetTaxRateByLocationAsync(It.IsAny<TaxByLocationQuery>()) as OkObjectResult;
            var data = actualResponse.Value as GetTaxRateByLocationResponse;

            Assert.NotNull(data);
            Assert.NotNull(actualResponse);
            Assert.NotNull(data.Rate);
            Assert.Equal("SomeCity", data.Rate.City);
            Assert.Equal("33647", data.Rate.Zip);
            Assert.Equal("1", data.Rate.CombinedDistrictRate);
        }

        [Theory]
        [InlineData(19, "USA", "AU")]
        public async void CalculateOrderSalesTax_ValidInput_Expect_Success(int amount, string fromCountry, string toCountry)
        {
            // Setup 
            var mockResponse = new SalesTaxByOrderResponse()
            {
                SalesTax = new SalesTax
                {
                    AmountToCollect = 14.80,
                    OrderTotalAmount = 130.88,
                    Breakdown = new Breakdown
                    {
                        CityTaxableAmount = 39,
                        CityTaxRate = 7
                    },
                    Jurisdictions = new Jurisdictions
                    {
                        City = "SomeCIty",
                        Country = "US"
                    }
                }
            };
            var fakeRequest = new OrderSalesTaxCommand()
            {
                Amount = amount,
                FromAddress = new Address
                {
                    Country = "US"
                },
                ToAddress = new Address
                {
                    Country = "US"
                }
            };

            _taxProvider.Setup(x => x.CalculateSalesTaxByOrder(It.IsAny<OrderSalesTaxCommand>()))
                .Returns(Task.FromResult(mockResponse)).Verifiable();

            // Act 
            var actualResponse = await _taxController.CalculateSalesTaxByOrderAsync(fakeRequest) as OkObjectResult;
            var data = actualResponse.Value as SalesTaxByOrderResponse;

            // Assert
            Assert.NotNull(actualResponse);
            Assert.NotNull(data);
            Assert.NotNull(data.SalesTax);
            Assert.Equal(14.80, data.SalesTax.AmountToCollect);
            Assert.Equal(130.88, data.SalesTax.OrderTotalAmount);
            Assert.NotNull(data.SalesTax.Breakdown);
            Assert.Equal(39, data.SalesTax.Breakdown.CityTaxableAmount);
            Assert.NotNull(data.SalesTax.Jurisdictions);
            Assert.Equal("SomeCIty", data.SalesTax.Jurisdictions.City);
        }

    }
}