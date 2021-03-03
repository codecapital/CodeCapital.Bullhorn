namespace CodeCapital.Bullhorn.Api
{
    public class BullhornSettings
    {
        /// <summary>
        /// Needed for Authorisation
        /// </summary>
        public string AuthorizationParameter { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string ClientId { get; set; } = default!;
        public string Secret { get; set; } = default!;
        public string LoginUrl { get; set; } = default!;
        public string TokenUrl { get; set; } = default!;
        public string AuthorizeUrl { get; set; } = default!;
    }
}
