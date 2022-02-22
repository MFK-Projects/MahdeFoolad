using NSMangament.Application.Models;
using NSMangament.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices.AccountManagement;
using Newtonsoft.Json;
using MahdeFooald.Common;
using Serilog;

namespace NSManagament.Infrastrucure.Impelementions
{
    public class UserManagerService : IUserMananger
    {

        private UserModel _user;

        private readonly IWebRequest _webRequest;
        public UserModel User
        {
            get => _user;
        }

        public UserManagerService(IWebRequest webRequest)
        {

            var check = CheckSettingFile();

            if (check.Item1 && check.Item2 != null)
                _user = check.Item2;
            else
            {
                if (check.Item2 != null)
                    _user = check.Item2;
                else
                    _user = new UserModel
                    {
                        FullName = String.Empty,
                        Password = String.Empty,
                        UserId = String.Empty,
                        UserName = GetUserName()
                    };
            }

            _webRequest = webRequest;
        }
        
        
        public async Task<bool> RetriveFromCRM()
        {
            var _jsondata = await _webRequest.DownlaodStringData(new DownloadStringModel
            {
                Password = _user.Password,
                Url = RequestUrl.BuildUrl(UrlBuilderMode.SingleUser, _user.UserName, null, null),
                UserName = _user.CredentialName,
                DomainName = RequestUrl.DomainName,
            });

            var userdata = JsonConvert.DeserializeObject<RootListModel<UserModel>>(_jsondata);

            if (userdata == null) return await Task.FromResult(false);

            var tempuser = userdata.Entities.FirstOrDefault();

            _user.FullName = tempuser.FullName;
            _user.UserId = tempuser.UserId;
            return await Task.FromResult(true);
        }
        public Task<bool> CheckPassword(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                throw new Exception(ExceptionMessages.NUllArgumentException(
                    new Type[]
                    {
                        typeof(string),
                        typeof(string)
                    },
                    new object[]
                    {
                        nameof(userName),
                        nameof(password)
                    }
                ));


            using var contextPrincipal = new PrincipalContext(ContextType.Machine);

            if (contextPrincipal.ValidateCredentials(userName, password))
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }
        public Task<bool> CheckPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(ExceptionMessages.NUllArgumentException(typeof(string), password));

            using var contextPrincipal = new PrincipalContext(ContextType.Machine);

            if (contextPrincipal.ValidateCredentials(User.UserName, password))
                return Task.FromResult(true);
            else
                return Task.FromResult(false);
        }
        public void SetUserInfo(UserModel user)
        {
            try
            {
                if (user == null)
                    throw new ArgumentNullException(ExceptionMessages.NUllArgumentException(typeof(UserModel), user));

                var _jsondata = JsonConvert.SerializeObject(user);
                System.IO.File.WriteAllText(CreateUserFile(), _jsondata);
                _user = user;
            }
            catch
            {
                throw new Exception("SetUserInfo Method thrown an exception Writing user data to file is failed!");
            }
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
        private string ConvertCurentUserName(string username)
        {
            if (username.Contains(@"KIAN\"))
                return username;
            else if (username.Contains("@"))
                return @"KIAN\" + username.Split("@")[0];

            return string.Empty;
        }
        private string GetUserName()
        {
            try { return ConvertCurentUserName(UserPrincipal.Current.UserPrincipalName); }
            catch { throw new Exception($"Can not Get the Curent UserName From the Active Directory in {nameof(GetUserName)} Method."); }
        }
        private (bool, UserModel) CheckSettingFile()
        {
            var _jsondata = System.IO.File.ReadAllText(CreateUserFile());

            if (_jsondata.Length == 0)
                return (false, null);

            UserModel _usermodel = JsonConvert.DeserializeObject<UserModel>(_jsondata);

            if (string.IsNullOrEmpty(_usermodel.Password))
                return (false, _usermodel);

            return (true, _usermodel);

        }
    }
}
