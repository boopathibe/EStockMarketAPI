using CompanyMicroService.Entities;
using CompanyMicroService.Models;
using CompanyMicroService.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyMicroService.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public List<CompanyResponseModel> GetAll()
        {
            return this._companyRepository.Get().Select(company => new CompanyResponseModel
            {
                Code = company.Code,
                CeoName = company.CeoName,
                Name = company.Name,
                TurnOver = company.TurnOver,
                Website = company.Website,
                Exchange = company.Exchange
            }).ToList();
        }

        public CompanyResponseModel GetByCode(string code)
        {
            var company =  this._companyRepository.GetByCode(code);
            if(company == null)
            {
                return null;
            }

            return new CompanyResponseModel
            {
                Code = company.Code,
                CeoName = company.CeoName,
                Name = company.Name,
                TurnOver = company.TurnOver,
                Website = company.Website,
                Exchange = company.Exchange
            };
        }

        public CompanyRequestModel Register(CompanyRequestModel company)
        {
            var companyEntity = new Company()
            {
                Code = company.Code,
                CeoName = company.CeoName,
                Name = company.Name,
                TurnOver = company.TurnOver,
                Website = company.Website,
                Exchange = company.Exchange,
                CreatedAt = DateTime.UtcNow
            };

            this._companyRepository.Create(companyEntity);
            return company;
        }

        public void Delete(string companycode)
        {
            _companyRepository.Delete(companycode);
        }
    }
}
