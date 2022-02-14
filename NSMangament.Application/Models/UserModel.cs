namespace NSMangament.Application.Models
{
    public class UserModel
    {
        public string CredentialName { get => UserName?.Split(@"\")[1] ?? string.Empty; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string OwnerId { get; set; }
    }


}
