using Newtonsoft.Json;

namespace NSMangament.Application.Models
{
    public class UserModel
    {
        public string CredentialName { get => UserName?.Split(@"\")[1] ?? string.Empty; }

        public string UserName { get; set; }


        public string Password { get; set; }

        [JsonProperty("fullname")]
        public string FullName { get; set; }


        [JsonProperty("ownerid")]
        public string UserId { get; set; }

    }


}
