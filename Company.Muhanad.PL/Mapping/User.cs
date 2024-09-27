using AutoMapper;
using Company.Muhanad.DAL.Models;
using Company.Muhanad.PL.ViewModels;

namespace Company.Muhanad.PL.Mapping
{
	public class User : Profile
	{
        public User()
        {
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
        }
    }
}
