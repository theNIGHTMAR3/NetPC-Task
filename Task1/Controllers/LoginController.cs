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

		public IActionResult Index()
		{
			return View();
		}
		//handles logging into application
		public IActionResult LoginProcess(UserModel tempUser)
		{

			UserAuthentication authentication = new();

			//if login and password is correct
			if(authentication.IsUserValid(tempUser))
			{
				return View("LoginSuccess", tempUser);
			}
			//else input data is uncorrect
			else
			{
				return View("LoginFailed", tempUser);
			}

			
		}
	}
}
