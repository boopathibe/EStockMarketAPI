using Authentication.Entities;
using Authentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface ILoginServices
    {
        bool IsValidUser(LoginModel loginModel);
    }
}
