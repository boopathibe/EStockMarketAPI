using Authentication.Entities;
using Authentication.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class LoginServices : ILoginServices
    {
        private readonly IMongoCollection<UserLogin> _userLogin;
        public LoginServices(IUserLoginDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _userLogin = database.GetCollection<UserLogin>(settings.UserloginCollectionName);
        }

        public bool IsValidUser(LoginModel loginModel)
        {
            ////var userName = this._userLogin.Find(x => x.UserName == loginModel.UserName).FirstOrDefault();
            ////var password = this._userLogin.Find(x => x.Password == loginModel.Password).FirstOrDefault();   
            var userDetails = this._userLogin.Find(x => true).FirstOrDefault();
            if (userDetails.UserName == loginModel.UserName && userDetails.Password == loginModel.Password)
            {
                return true;
            }
            return false;
        }
    }
    
}

