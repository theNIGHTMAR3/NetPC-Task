using System;
using System.Data.SqlClient;
using Task1.Models;

namespace Task1.Utils
{
	public class UsersDAO
	{
		// connectionString to connect to RegisterdUsers database
		string connectionString = @" Data Source=(localdb)\MSSQLLocalDB;Initial Catalog = RegisteredUsers; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";

		bool done = false;


		public bool FindUserByNameAndHash(UserModel user)
		{
			// query to find user in database
			string query = "SELECT * FROM Users WHERE USERNAME= @username AND PASSWORDHASH=@passwordHash";
			
			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new SqlCommand(query, connection);
				cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar,30).Value=user.Username;
				cmd.Parameters.Add("@passwordHash", System.Data.SqlDbType.VarChar, 64).Value = user.passwordHash;


				// try to open connection with DB
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.HasRows)
					{
						done = true;
					}
						
				}
				catch(Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
				}

			}

			return done;
		}x

		// checks if user with given login exists in DB
		public bool FindUserByLogin(string userName)
		{
			// query to find login in database
			string query = "SELECT * FROM Users WHERE USERNAME= @username";

			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new SqlConnection(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new SqlCommand(query, connection);
				cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 30).Value = userName;


				// try to open connection with DB
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					if (reader.HasRows)
					{
						done = true;
					}

				}
				catch (Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
				}

			}

			return done;
		}

	}

	
}
