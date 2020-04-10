using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourcehubApiDotNet.Models
{
    public class User
    {
        [Key]
        public string username { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string email { get; set; }
        public bool isEmailVerified { get; set; }
        public string avatar { get; set; }
        public string primaryOauthProvider { get; set; }
    }
}