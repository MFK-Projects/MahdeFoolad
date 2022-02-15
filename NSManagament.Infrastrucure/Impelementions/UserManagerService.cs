using NSMangament.Application.Models;
using NSMangament.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using Newtonsoft.Json;

namespace NSManagament.Infrastrucure.Impelementions
{
    public class UserManagerService : IUserMananger
    {

        private readonly UserModel _user;
        public UserModel User
        {
            get => _user;
        }

        public UserManagerService()
        {
            if (_user == null)
                _user = new UserModel();
        }



        public Task<bool> ChecckPassword(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return Task.FromResult(false);


            using var contextPrincipal = new PrincipalContext(ContextType.Machine);

            if (contextPrincipal.ValidateCredentials(userName, password))
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }
        public Task<bool> CheckPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return Task.FromResult(false);

            using var contextPrincipal = new PrincipalContext(ContextType.Machine);

            if (contextPrincipal.ValidateCredentials(User.UserName, password))
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }


        private void SetUserInfo(UserModel user)
        {
            var _jsondata = JsonConvert.SerializeObject(user);
            System.IO.File.WriteAllText(CreateUserFile(), _jsondata);
        }
        private string CreateUserFile()
        {
            string path = Environment.CurrentDirectory + @"\UserInfo.json";

            if (!System.IO.File.Exists(path))
            {
                System.IO.File.Create(path)?.Dispose();
                return path;
            }
            else
                return path;
        }
        private string CredentialUserName(string username)
        {
            if (username.Contains(@"KIAN\"))
                return username;
            else if (username.Contains("@"))
                return @"KIAN\" + username.Split("@")[0];

            return string.Empty;
        }
    }
}
