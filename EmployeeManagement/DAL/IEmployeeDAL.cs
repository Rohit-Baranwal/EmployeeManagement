using System.Collections.Generic;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL
{
    public interface IEmployeeDAL
    {
        List<Employee> GetEmployees();
        void AddEmployee(Employee emp);
        void UpdateEmployee(Employee emp);
        void DeleteEmployee(int id);
    }
}
