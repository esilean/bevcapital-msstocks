using Newtonsoft.Json;

namespace BevCapital.Stocks.Application.UseCases.Stocks.Response
{
    public class StockOut
    {
        [JsonProperty("symbol")]
        public string Id { get; set; }
        public string StockName { get; set; }
        public string Exchange { get; set; }
        public string Website { get; set; }
        public StockPriceOut StockPrice { get; set; }
    }
}
