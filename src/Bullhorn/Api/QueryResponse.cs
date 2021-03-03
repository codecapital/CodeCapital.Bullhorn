using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using CodeCapital.Bullhorn.Dtos;

namespace CodeCapital.Bullhorn.Api
{
    public class QueryResponse : ErrorResponseDto
    {
        public int Total { get; set; }
        public int Start { get; set; }
        public int Count { get; set; }
        public List<JObject> Data { get; set; }
    }
}
