namespace BevCapital.Stocks.Application.UseCases.Stocks.Response
{
    public class StockOut
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Exchange { get; set; }
        public string Website { get; set; }
        public StockPriceOut StockPrice { get; set; }
    }
}
