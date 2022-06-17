using Dapper;
using DapperBP.Data;
using DapperBP.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperBP.Repository
{
    public class CompanyRepositoryDP : ICompanyRepository
    {
        private IDbConnection _db;

        public CompanyRepositoryDP(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Company Add(Company company)
        {
            var sql = "INSERT INTO Companies (Name, Address, City, State, PostalCode) VALUES(@Name, @Address, @City, @State, @PostalCode);"
                       + "SELECT CAST(SCOPE_IDENTITY() as int);";
            //var id = _db.Query<int>(sql, new 
            //{
            //    @Name = company.Name,
            //    @Address = company.Address,
            //    @City = company.City,
            //    @State = company.State,
            //    @PostalCode = company.PostalCode
            //}).Single();

            //or
            var id = _db.Query<int>(sql, company).Single();

            company.CompanyId = id;
            return company;
        }

        public Company Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId";
            return _db.Query<Company>(sql, new { @CompanyId = id }).Single();
        }

        public List<Company> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            return _db.Query<Company>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Companies WHERE CompanyId = @Id";
            _db.Execute(sql, new { @id = id });
        }

        public Company Update(Company company)
        {
            var sql = "UPDATE Companies SET Name = @Name, Address = @Address, City = @City, State = @State, PostalCode = @PostalCode WHERE CompanyId = @CompanyId";
           
            // if you will not retrieve anything use Execute
            _db.Execute(sql, company);
            return company;

        }
    }
}
