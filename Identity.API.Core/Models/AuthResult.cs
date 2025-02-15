namespace Identity.API.Core.Models
{
    public class AuthResult
    {
        public bool Successful { get; set; }
        public string Token { get; set; } = string.Empty;
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
    }
}
