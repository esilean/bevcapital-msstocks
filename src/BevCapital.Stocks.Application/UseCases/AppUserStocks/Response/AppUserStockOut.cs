using BevCapital.Stocks.Application.UseCases.Stocks.Response;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BevCapital.Stocks.Application.UseCases.AppUserStocks.Response
{
    public class AppUserStockOut
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        [JsonPropertyName("stocks")]
        public List<StockOut> StockOuts { get; set; }
    }
}
