namespace CodeCapital.Bullhorn.Dtos
{
    public class ErrorResponseDto
    {
        public string ErrorMessage { get; set; } = "";
        public string ErrorMessageKey { get; set; } = "";
        public int ErrorCode { get; set; }
        public bool Success => string.IsNullOrWhiteSpace(ErrorMessage) && ErrorCode == 0;
    }
}
