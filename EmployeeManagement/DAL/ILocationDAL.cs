using System.Collections.Generic;
using EmployeeManagement.Models;

namespace EmployeeManagement.DAL
{
    public interface ILocationDAL
    {
        List<State> GetStates();
        List<City> GetCitiesByState(int stateId);
    }
}
