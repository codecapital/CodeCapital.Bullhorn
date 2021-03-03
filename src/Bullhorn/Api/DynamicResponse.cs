using CodeCapital.Bullhorn.Dtos;

namespace CodeCapital.Bullhorn.Api
{
    public class DynamicResponse : ErrorResponseDto
    {
        public int Total { get; set; }
        public string Json { get; set; } = "";
        public string RequestUri { get; set; } = "";
    }
}
