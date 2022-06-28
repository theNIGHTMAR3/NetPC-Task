using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task1.Models;
using Task1.Security;
using Task1.Utils;

namespace Task1.Controllers
{
	public class RegistrationController : Controller
	{
	
		public IActionResult Index()
		{
			return View();
		}
		// handles new user registration 
		public IActionResult RegistrationProcess(UserModel user)
		{

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
				// return View("Index", user,"test");
				ViewBag.ErrorMessage = "Input error";
				return View("RegistrationFailed", user);
			}

		}
	}
}
