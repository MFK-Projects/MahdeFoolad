using Newtonsoft.Json;

namespace NSMangament.Application.Models
{
    public class UserModel
    {
        [JsonIgnore]
        public string CredentialName { get => UserName?.Split(@"\")[1] ?? string.Empty; }


        [JsonIgnore]
        public string UserName { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        [JsonProperty("fullname")]
        public string FullName { get; set; }


        [JsonProperty("ownerid")]
        public string UserId { get; set; }

    }


}
