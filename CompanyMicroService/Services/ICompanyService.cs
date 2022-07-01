using CompanyMicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Services
{
    public interface ICompanyService
    {
        List<CompanyResponse> GetAll();

        CompanyResponse GetByCode(string code);

        CompanyRequest Register(CompanyRequest company);

        void Delete(string code);
    }
}
