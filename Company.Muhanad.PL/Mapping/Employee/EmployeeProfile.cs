using AutoMapper;
using Company.Muhanad.DAL.Models;
using Company.Muhanad.PL.ViewModels;

namespace Company.Muhanad.PL.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
           
        }
    }
}
