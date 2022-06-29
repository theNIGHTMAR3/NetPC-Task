using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Task1.Models;
using Task1.Security;
using Task1.Utils;

namespace Task1.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			// if someone is already logged in
			if(HttpContext.Session.GetString("user")!=null)
			{
				// redirect to main page
				return View("../Home/Index");
			}
			return View();
		}
		//handles logging into application
		public IActionResult LoginProcess(UserModel tempUser)
		{
			// if someone is already logged in
			if (HttpContext.Session.GetString("user") != null)
			{
				// redirect to main page
				return View("../Home/Index");
			}

			UsersDAO userDAO = new();

			// find user by login and password
			UserModel foundUser = userDAO.FindUserByNameAndHash(tempUser);

			// if user is not found found 
			if (foundUser==null)
			{
				return View("LoginFailed", tempUser);
			}
			//else input data is uncorrect
			else
			{
				// set session to logged user
				HttpContext.Session.SetString("user", foundUser.Username);

				return View("LoginSuccess", foundUser);
			}
		}
		// clear session after logout
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			return View("../Home/Index");
		}
	}
}
