using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourcehubApiDotNet.Models
{
    /*
    *   Id                - Table's primary key
    *   usernameFK        - Foreign key from User
    *   platformUserId    - UserId on the oauth platform
    *   platformUsername  - Username on the oauth platform
    *   platformAvatar    - Avatar on the oauth platform
    *   accessToken       - OAuth access token
    *   accessTokenSecret - OAuth access token secret
    */
    public class OAuthTwitter 
    {
        public int Id { get; set; }
        [ForeignKey("User")]
        public string usernameFK { get; set; }
        public int platformUserId { get; set; }
        public string platformUsername { get; set; }
        public string platformAvatar { get; set; }
        public string accessToken { get; set; }
        public string accessTokenSecret { get; set; }
    }
}