using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task1.Models;
using Task1.Security;

namespace Task1.Controllers
{
	public class LoginController : Controller
	{
		//private readonly ILogger<HomeController> _logger;

		public IActionResult index()
		{
			return View();
		}
		//handles logging into application
		public IActionResult LoginProcess(UserModel user)
		{

			UserAuthentication authentication = new UserAuthentication();

			//if login and password is correct
			if(authentication.isUserValid(user))
			{
				return View("LoginSuccess", user);
			}
			//else input data is uncorrect
			else
			{
				return View("LoginFailed", user);
			}

			
		}
	}
}
