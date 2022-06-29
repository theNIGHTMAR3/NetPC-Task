using Task1.Models;
using Task1.Utils;

namespace Task1.Security
{
	// authenticates users after login
	public class UserAuthentication
	{
		readonly UsersDAO usersDAO = new ();

		public UserAuthentication(){ }

		// checks if given user is registered
		public bool IsUserValid(UserModel user)
		{
			// return bool computed with a querry to DB 
			return usersDAO.FindUserByNameAndHash(user)==null;
		}

		// checks if given user login is available to proceed registration
		public bool IsLoginAvailable(UserModel user)
		{
			// return negated querry search
			if (usersDAO.FindUserByLogin(user.Username) == null)
			{
				return true;
			}	
			else
			{
				return false;
			}
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