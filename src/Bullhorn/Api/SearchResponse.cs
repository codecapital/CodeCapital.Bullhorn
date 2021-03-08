using System.Collections.Generic;
using System.Text.Json;

namespace CodeCapital.Bullhorn.Api
{
    public class SearchResponse
    {
        public int Total { get; set; }
        public int Start { get; set; }
        public int Count { get; set; }
        public List<JsonDocument> Data { get; set; }
        public decimal _Score { get; set; }
    }

    public class SearchResponse2<T>
    {
        public int Total { get; set; }
        public int Start { get; set; }
        public int Count { get; set; }
        public List<T> Data { get; set; }
        public decimal _Score { get; set; }
    }
}
