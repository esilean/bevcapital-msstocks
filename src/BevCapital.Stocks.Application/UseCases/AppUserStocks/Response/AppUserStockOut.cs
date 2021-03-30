using BevCapital.Stocks.Application.UseCases.Stocks.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BevCapital.Stocks.Application.UseCases.AppUserStocks.Response
{
    public class AppUserStockOut
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("stocks")]
        public List<StockOut> StockOuts { get; set; }
    }
}
