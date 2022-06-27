using System.Collections.Generic;
using System.Security.Cryptography;
using Task1.Models;

namespace Task1.Security
{
	//authenticates users after login
	public class UserAuthentication
	{
		List<UserModel> registredUsers = new List<UserModel>();

		// authentication constructor with some hard-coded users
		public UserAuthentication()
		{
			// initalize sha256
			using (SHA256 mySHA256 = SHA256.Create())
			{
				//compute password hash
				string hash1 = Utilities.GetHash(mySHA256, "test");
				string hash2 = Utilities.GetHash(mySHA256, "MK");

				//create
				registredUsers.Add(new UserModel("test", hash1));
				registredUsers.Add(new UserModel("MichałKuprianowicz", hash2));
			}
		}


		//checks of given user is registered
		public bool isUserValid(UserModel user)
		{
			foreach(UserModel temp in registredUsers)
			{
				//check if username is known
				if(temp.Username==user.Username)
				{
					//check if hashes are the same
					//usernames are unique so hash doesn't match return false
					if (temp.PasswordHash == user.PasswordHash)
						return true;
					else 
						return false;
				}
			}
			//if not found
			return false;
		}
	}
}