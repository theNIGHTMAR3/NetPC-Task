using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1.Models;
using Task1.Security;
using Task1.Utils;

namespace Task1.Controllers
{
	public class RegistrationController : Controller
	{
	
		public IActionResult Index()
		{
			// if someone is already logged in
			if (HttpContext.Session.GetString("user") != null)
			{
				// redirect to main page
				return View("../Home/Index");
			}

			return View();
		}
		// handles new user registration 
		public IActionResult RegistrationProcess(UserModel user)
		{
			// if someone is already logged in
			if (HttpContext.Session.GetString("user") != null)
			{
				// redirect to main page
				return View("../Home/Index");
			}

			UserAuthentication authentication = new();

			// if login and password are correct to add new user
			if (authentication.IsLoginAvailable(user) && Utilities.IsPasswordStrongEnough(user.PasswordHash))
			{
				// check if DB handled querry
				if(authentication.RegisterNewUser(user))
				{
					return View("RegistrationSuccess", user);
				}
				else
				{
					ViewBag.ErrorMessage = "DB error";
					return View("RegistrationFailed", user);
				}

			}
			// else input data is incorrect
			else
			{
				return View("RegistrationFailed", user);
			}

		}
	}
}
