using DapperBP.Data;
using DapperBP.Models;

namespace DapperBP.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        private readonly ApplicationDbContext _appDb;

        public CompanyRepositoryEF(ApplicationDbContext appDb)
        {
            _appDb = appDb;
        }

        public Company Add(Company company)
        {
            _appDb.Companies.Add(company);
            _appDb.SaveChanges();
            return company;
        }

        public Company Find(int id)
        {
            return _appDb.Companies.FirstOrDefault(x => x.CompanyId == id);

        }

        public List<Company> GetAll()
        {
            return _appDb.Companies.ToList();
        }

        public void Remove(int id)
        {
            Company company = _appDb.Companies.FirstOrDefault(x => x.CompanyId == id);
            _appDb.Companies.Remove(company);
            _appDb.SaveChanges();
            return;
        }

        public Company Update(Company company)
        {
            _appDb.Companies.Update(company);
            _appDb.SaveChanges();
            return company;
        }
    }
}
