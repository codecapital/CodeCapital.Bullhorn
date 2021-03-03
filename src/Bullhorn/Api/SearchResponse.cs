using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace CodeCapital.Bullhorn.Api
{
    public class SearchResponse
    {
        public int Total { get; set; }
        public int Start { get; set; }
        public int Count { get; set; }
        public List<JObject> Data { get; set; }
        public decimal _Score { get; set; }
    }
}
