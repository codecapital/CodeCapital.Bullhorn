using System.Collections.Generic;

namespace CodeCapital.Bullhorn.Api
{
    public class DynamicQueryResponse : DynamicResponse
    {
        public int Start { get; set; }
        public int Count { get; set; }
        public List<dynamic> Data { get; set; }
        public List<dynamic> DynamicData { get; set; }
    }
}
