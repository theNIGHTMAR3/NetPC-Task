using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Task1.Models;
using Task1.Utils;

namespace Task1.Security
{
	// authenticates users after login
	public class UserAuthentication
	{
		readonly UsersDAO usersDAO = new ();

		// authentication constructor with some hard-coded users
		public UserAuthentication()
		{
	
		}

		// checks if given user is registered
		public bool IsUserValid(UserModel user)
		{
			// return bool computed with a querry to DB 
			return usersDAO.FindUserByNameAndHash(user);
		}

		// checks if given user login is available to proceed registarion
		public bool IsLoginAvailable(UserModel user)
		{
			// return negated querry search
			return !usersDAO.FindUserByLogin(user.Username);
		}

		// cheks if given password is strong enough
		public bool IsPasswordStrongEnough(string password)
		{
			// is long enough
			if (password.Length<7)
			{
				return false;
			}
			// contains at leat 1 Capital letter
			if (!password.Any(char.IsUpper))
			{
				return false;
			}
			// contains at leat 1 digit
			if (!password.Any(char.IsDigit))
			{
				return false;
			}
			// does not contain white space	
			if (password.Contains(" "))
			{
				return false;
			}
			
			return true;
		}

		// register new user to application
		public bool RegisterNewUser(UserModel user)
		{
			// add new user to DB
			if(usersDAO.AddUserToDataBase(user))
			{
				return true;
			}
			else 
			{
				// error when something went wrong with DB
				return false;
			}
		}


	}
}