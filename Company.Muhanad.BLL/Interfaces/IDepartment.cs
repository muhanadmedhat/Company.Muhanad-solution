using Company.Muhanad.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Muhanad.BLL.Interfaces
{
    public interface IDepartment : IGenericRepository<Department>
    {
       
     public Task<IEnumerable<Department>> GetByNameAsync(string name);

    }
}
