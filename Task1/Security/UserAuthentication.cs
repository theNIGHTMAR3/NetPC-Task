using System.Collections.Generic;
using System.Security.Cryptography;
using Task1.Models;
using Task1.Utils;

namespace Task1.Security
{
	// authenticates users after login
	public class UserAuthentication
	{
		readonly List<UserModel> registredUsers = new();
		readonly UsersDAO usersDAO = new ();

		// authentication constructor with some hard-coded users
		public UserAuthentication()
		{
			// initalize sha256
			using (SHA256 mySHA256 = SHA256.Create())
			{
				// compute password hash
				string hash1 = Utilities.GetHash(mySHA256, "test");
				string hash2 = Utilities.GetHash(mySHA256, "MK");

				// create
				registredUsers.Add(new UserModel("test", hash1));
				registredUsers.Add(new UserModel("MichałKuprianowicz", hash2));
			}
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
			if (password.Length<7)
			{
				return false;
			}
			

			return true;
		}
	}
}