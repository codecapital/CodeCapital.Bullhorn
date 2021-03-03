using CodeCapital.Bullhorn.Dtos;

namespace CodeCapital.Bullhorn.Api
{
    public class EntityResponse<T> : ErrorResponseDto
    {
        public T Data { get; set; }
    }
}
