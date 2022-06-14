using CompanyMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Services
{
    public interface ICompanyService
    {
        List<CompanyResponseModel> GetAll();

        CompanyResponseModel GetByCode(string code);

        CompanyRequestModel Register(CompanyRequestModel company);

        void Delete(string code);
    }
}
