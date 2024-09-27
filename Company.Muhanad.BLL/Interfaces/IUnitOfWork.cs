﻿using Company.Muhanad.BLL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Muhanad.BLL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployee employeeRepository { get;  }
        public IDepartment departmentRepository { get; }
    }
}
