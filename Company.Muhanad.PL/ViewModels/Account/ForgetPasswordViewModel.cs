using System.ComponentModel.DataAnnotations;

namespace Company.Muhanad.PL.ViewModels.Account
{
	public class ForgetPasswordViewModel
	{
		[Required(ErrorMessage = "Email is required")]
		[EmailAddress]
		public string Email { get; set; }
	}
}
