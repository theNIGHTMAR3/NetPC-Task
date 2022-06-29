using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Task1.Models;

namespace Task1.Utils
{
	// DAO - data access object, contains methods executing queries on Users table
	public class UsersDAO
	{
		// connectionString to connect to RegisterdUsers database
		readonly string connectionString = @" Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=NetPC-Task1;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		
		public UserModel FindUserByNameAndHash(UserModel user)
		{

			// query to find user in database
			string query = "SELECT * FROM Users WHERE USERNAME= @username AND PASSWORDHASH=@passwordHash";
			
			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new(query, connection);


				// initalize sha256
				using (SHA256 mySHA256 = SHA256.Create())
				{
					// compute password hash
					string hash = Utilities.GetHash(mySHA256, user.PasswordHash);

					// add parameter to querry
					cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 30).Value = user.Username;
					cmd.Parameters.Add("@passwordHash", System.Data.SqlDbType.VarChar, 64).Value = hash;
				}

				// try to open connection with DB
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();

					reader.Read();
					UserModel foundUser = (
						new UserModel
						{
							ID = (int)reader[0],
							Username = (string)reader[1],
							PasswordHash = (string)reader[2],
						});
					return foundUser;

				}
				catch(Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
				}
			}

			return null;
		}

		// checks if user with given login exists in DB
		public UserModel FindUserByLogin(string userName)
		{
			// query to find login in database
			string query = "SELECT * FROM Users WHERE USERNAME= @username";

			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new(query, connection);
				cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 30).Value = userName;


				// try to open connection with DB
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();

					// create new user from DB
					UserModel foundUser = (
						new UserModel
						{
							ID = (int)reader[0],
							Username = (string)reader[1],
							PasswordHash = (string)reader[2],
						});
					return foundUser;
				}
				catch (Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
				}

			}

			return null;
		}

		// adds new user to DB
		public bool AddUserToDataBase(UserModel newUser)
		{
			// query to find user in database
			string query = "INSERT INTO Users(USERNAME,PASSWORDHASH) VALUES(@username,@passwordHash);";

			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new(query, connection);

				// initalize sha256
				using (SHA256 mySHA256 = SHA256.Create())
				{
					// compute password hash
					string hash = Utilities.GetHash(mySHA256, newUser.PasswordHash);

					// add parameter to querry
					cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 30).Value = newUser.Username;
					cmd.Parameters.Add("@passwordHash", System.Data.SqlDbType.VarChar, 64).Value = hash;
				}

				// try to open connection with DB
				try
				{
					connection.Open();
					// execute querry
					cmd.ExecuteNonQuery();
					return true;

				}
				catch (Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
					return false;
				}
			}
		}

	}

	
}
