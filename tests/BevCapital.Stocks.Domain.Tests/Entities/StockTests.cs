using BevCapital.Stocks.Domain.Entities;
using Xunit;

namespace BevCapital.Stocks.Domain.Tests.Entities
{
    public class StockTests
    {
        [Fact(DisplayName = "It should create a stock successfully")]
        public void Stock_ShouldCreate_A_StockWithPrice()
        {
            // ARRANGE
            var symbol = "symbol";
            var name = "name";
            var exchange = "exchange";
            var website = "website";

            // ACT
            var stock = Stock.Create(symbol, name, exchange, website);

            // ASSERT
            Assert.Equal(symbol, stock.Id);
            Assert.Equal(name, stock.StockName);
            Assert.Equal(exchange, stock.Exchange);
            Assert.Equal(website, stock.Website);
        }

        [Theory(DisplayName = "It should throw an error if stock was not created")]
        [InlineData(null, "", "", "")]
        [InlineData("", null, "", "")]
        [InlineData("", "", null, "")]
        [InlineData("", "", "", null)]
        [InlineData(null, null, null, null)]
        public void Stock_ShouldNotCreate_A_Stock_With_EmptyValues(string symbol, string name, string exchange, string website)
        {
            // ARRANGE
            // ACT
            var stock = Stock.Create(symbol, name, exchange, website);

            // ASSERT
            Assert.True(stock.Invalid);
        }
    }
}
