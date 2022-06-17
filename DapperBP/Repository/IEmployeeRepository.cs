using DapperBP.Models;

namespace DapperBP.Repository
{
    public interface IEmployeeRepository
    {
        Employee Find(int id);

        List<Employee> GetAll();

        Employee Add(Employee employee);

        Employee Update(Employee employee);

        void Remove(int id);    
    }
}
