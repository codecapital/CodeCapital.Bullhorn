using CodeCapital.Bullhorn.Dtos;
using System.Collections.Generic;
using System.Text.Json;

namespace CodeCapital.Bullhorn.Api
{
    public class QueryResponse : ErrorResponseDto
    {
        public int Total { get; set; }
        public int Start { get; set; }
        public int Count { get; set; }
        //ToDo Json
        public List<JsonDocument> Data { get; set; }
    }
}
