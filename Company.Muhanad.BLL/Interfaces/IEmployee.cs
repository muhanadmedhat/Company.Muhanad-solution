using Company.Muhanad.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Muhanad.BLL.Interfaces
{
    public interface IEmployee : IGenericRepository<Employee>
    {
       
        Task<IEnumerable<Employee>> GetByNameAsync(string name);
    }
}
