using CompanyMicroService.Entities;
using CompanyMicroService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyMicroService.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly IMongoCollection<Company> _companies;
        public CompanyRepository(ICompanyDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _companies = database.GetCollection<Company>(settings.CompanyCollectionName);
        }

        public List<Company> Get()
        {
            return this._companies.Find(comp => true).ToList();
        }

        public Company GetByCode(string code)
        {
            return this._companies.Find(comp => comp.Code == code).FirstOrDefault();
        }

        public Company Create(Company company)
        {
            _companies.InsertOne(company);
            return company;
        }

        public void Delete(string code)
        {
            _companies.DeleteOne(comp => comp.Code == code);
        }
    }
}
