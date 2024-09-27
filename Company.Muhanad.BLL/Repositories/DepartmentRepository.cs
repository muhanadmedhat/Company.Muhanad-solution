using Company.Muhanad.BLL.Interfaces;
using Company.Muhanad.DAL.Data.Contexts;
using Company.Muhanad.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Muhanad.BLL.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>,IDepartment
    {
      
        public DepartmentRepository(AppDBContext context) : base(context)
        {
            
        }
      
    }
}
