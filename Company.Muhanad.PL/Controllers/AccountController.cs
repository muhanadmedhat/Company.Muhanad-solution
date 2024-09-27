using Company.Muhanad.DAL.Models;
using Company.Muhanad.PL.Helpers;
using Company.Muhanad.PL.ViewModels;
using Company.Muhanad.PL.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Muhanad.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel model)
		{
			if (ModelState.IsValid)
            {
                var user =await _userManager.FindByNameAsync(model.UserName);
                if(user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if( user is null )
                    {
                        user=new ApplicationUser()
                        {
                            UserName= model.UserName,
                            FirstName= model.FirstName,
                            LastName= model.LastName,
                            Email= model.Email,
                            IsAgree= model.IsAgree
                           
                        };
                        var result = await _userManager.CreateAsync(user,model.Password);
                        if(result.Succeeded)
                        {
                            return RedirectToAction("SignIn");
                        }
                    }
                    ModelState.AddModelError(string.Empty, "Email is already used");
                }

                ModelState.AddModelError(string.Empty, "UserName is used");
                return View(model);
            }
			return View(model);
		}

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
					var user = await _userManager.FindByEmailAsync(model.Email);
					if (user is not null)
					{
						var flag = await _userManager.CheckPasswordAsync(user, model.Password);
						if (flag)
						{
							//here give authorization to user
							var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
							if (result.Succeeded)
							{
								return RedirectToAction("Index", "Home");

							}
						}
					}
					ModelState.AddModelError(string.Empty, "Invalid Login");
				}
				catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }

        public IActionResult ForgetPassword()
        {
            return View();
        }

        public async Task<IActionResult> SendPasswordUrl(ForgetPasswordViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user =await _userManager.FindByEmailAsync(model.Email);
                if(user is not null)
                {
                    var token =await _userManager.GeneratePasswordResetTokenAsync(user);

                    var url =Url.ActionLink("ResetPassword", "Account", new {email=model.Email , token},Request.Scheme);

                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password",
                        Body = url
					};
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CheckInbox");
                }
                ModelState.AddModelError(string.Empty, "Invalid Operation");
            }
            return View(model);
        }

        public IActionResult CheckInbox()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ResetPassword(string email,string token)
        {
            TempData["email"]=email;
            TempData["token"]=token;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = TempData["email"] as string;
                var token = TempData["token"] as string;

                var user =await _userManager.FindByEmailAsync(email);
                var flag =await _userManager.CheckPasswordAsync(user, model.Password);
                if (flag)
                {
					ModelState.AddModelError(string.Empty, "Invalid Operation");
					TempData["email"] = email;
					TempData["token"] = token;
					return View(model);
				}
                var result=await _userManager.ResetPasswordAsync(user,token,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("SignIn");
                }
                ModelState.AddModelError(string.Empty, "Invalid Operation");
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
	}
}
