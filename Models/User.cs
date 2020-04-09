namespace ResourcehubApiDotNet.Models
{
    public class User
    {
        public int Id { get; set; }
        [System.ComponentModel.DataAnnotations.Key]
        public string username { get; set; }
        public string email { get; set; }
        public bool isEmailVerified { get; set; }
        public string avatar { get; set; }
        public string primaryOauthProvider { get; set; }
    }
}