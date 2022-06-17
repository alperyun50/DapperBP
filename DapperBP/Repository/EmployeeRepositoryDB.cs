using Dapper;
using DapperBP.Data;
using DapperBP.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperBP.Repository
{
    public class EmployeeRepositoryDP : IEmployeeRepository
    {
        private IDbConnection _db;

        public EmployeeRepositoryDP(IConfiguration configuration)
        {
            _db = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public Employee Add(Employee employee)
        {
            var sql = "INSERT INTO Employees (Name, Title, Email, Phone, CompanyId) VALUES(@Name, @Title, @Email, @Phone, @CompanyId);"
                      + "SELECT CAST(SCOPE_IDENTITY() as int); ";
            //var id = _db.Query<int>(sql, new 
            //{
            //    @Name = company.Name,
            //    @Address = company.Address,
            //    @City = company.City,
            //    @State = company.State,
            //    @PostalCode = company.PostalCode
            //}).Single();

            //or
            var id = _db.Query<int>(sql, employee).Single();

            employee.EmployeeId = id;
            return employee;
        }

        public Employee Find(int id)
        {
            var sql = "SELECT * FROM Employees WHERE EmployeeId = @EmployeeId";
            return _db.Query<Employee>(sql, new { @EmployeeId = id }).Single();
        }

        public List<Employee> GetAll()
        {
            var sql = "SELECT * FROM Employees";
            return _db.Query<Employee>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Employees WHERE EmployeeId = @Id";
            _db.Execute(sql, new { @id = id });
        }

        public Employee Update(Employee employee)
        {
            var sql = "UPDATE Employees SET Name = @Name, Title = @Title, Email = @Email, Phone = @Phone, CompanyId = @CompanyId WHERE EmployeeId = @EmployeeId";

            // if you will not retrieve anything use Execute
            _db.Execute(sql, employee);
            return employee;

        }
    }
}
