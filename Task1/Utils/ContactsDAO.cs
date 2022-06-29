using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Task1.Models;

namespace Task1.Utils
{
	// DAO - data access object, contains methods executing queries on Contacts table
	public class ContactsDAO
	{
		// connectionString to connect to RegisterdUsers database
		readonly string connectionString = @" Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=myDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

		// return all contact from DB as a list
		public List<ContactModel> FindAllContacts()
		{
			List<ContactModel> foundContacts = new();

			// query to find whole DB
			string query = "SELECT * FROM Contacts";

			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new(connectionString))
			{
				// setting parameters to query
				SqlCommand cmd = new(query, connection);

				// try to open connection with DB
				try
				{
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					// create new contact from reader input
					while (reader.Read())
					{
						foundContacts.Add(
							new ContactModel 
							{ 
								ID=(int)reader[0],
								Name = (string)reader[1], 
								Surname = (string)reader[2], 
								Email = (string)reader[3], 
								Password = (string)reader[4], 
								Category = (string)reader[5], 
								PhoneNumber = (string)reader[6], 
								BirthDate = (DateTime)reader[7] 
							});
					}

				}
				catch (Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
				}

			}
			return foundContacts;
		}

		// delete contact from DB using it's unique id
		public void DeleteContactByID(int id)
		{
			// query to find login in database
			string query = "DELETE FROM Contacts WHERE ID= @id";

			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new(query, connection);
				cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;


				// try to open connection with DB
				try
				{
					connection.Open();
					// execute querry
					cmd.ExecuteNonQuery();


				}
				catch (Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
				}

			}

		}

		// adds new contact to DB
		public void AddNewContact(ContactModel newContact)
		{
			// query to find user in database
			string query = "INSERT INTO Contacts(Name,Surname,Email,Password,Category,Phone_Number,Birth_Date) VALUES(@name,@surname,@email,@password,@category,@phoneNumber,@birthDate);";

			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new(query, connection);

				cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 20).Value = newContact.Name;
				cmd.Parameters.Add("@surname", System.Data.SqlDbType.VarChar, 40).Value = newContact.Surname;
				cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 50).Value = newContact.Email;
				cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 20).Value = newContact.Password;
				cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar, 10).Value = newContact.Category;
				cmd.Parameters.Add("@phoneNumber", System.Data.SqlDbType.VarChar, 10).Value = newContact.PhoneNumber;
				cmd.Parameters.Add("@BirthDate", System.Data.SqlDbType.Date).Value = newContact.BirthDate;

				// try to open connection with DB
				try
				{
					connection.Open();
					// execute querry
					cmd.ExecuteNonQuery();
				}
				catch (Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
				}
			}
		}

		// checks if exists in DB contact with given email
		public bool CheckIfEmailExists(string email)
		{
			bool done = false;
			// query to find login in database
			string query = "SELECT * FROM Contacts WHERE Email= @email";

			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new(query, connection);
				cmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = email;


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

		// updates contact
		public void UpdateContact(ContactModel updatedContact)
		{

			// query to find login in database
			string query = "UPDATE Contacts SET Name=@name,Surname=@surname,Email=@email,Password=@password,Category=@category,Phone_Number=@phoneNumber,Birth_Date=@birthDat WHERE ID= @id";

			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new(query, connection);
				
				cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 20).Value = updatedContact.Name;
				cmd.Parameters.Add("@surname", System.Data.SqlDbType.VarChar, 40).Value = updatedContact.Surname;
				cmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 50).Value = updatedContact.Email;
				cmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 20).Value = updatedContact.Password;
				cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar, 10).Value = updatedContact.Category;
				cmd.Parameters.Add("@phoneNumber", System.Data.SqlDbType.VarChar, 10).Value = updatedContact.PhoneNumber;
				cmd.Parameters.Add("@birthDate", System.Data.SqlDbType.Date).Value = updatedContact.BirthDate;

				cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = updatedContact.ID;

				//does not work, getting null sql exception, spent 2h on that, enough
				// try to open connection with DB
				try
				{
					connection.Open();

					cmd.ExecuteNonQuery();		
				}
				catch (Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
				}
			}
		}

		// returns contact having given ID, NULL otherwise
		public ContactModel FindContactByID(int id)
		{

			// query to find login in database
			string query = "SELECT * FROM Contacts WHERE ID= @id";

			// create connection VARIABLE to DB and close it when finished
			using (SqlConnection connection = new(connectionString))
			{
				//setting parameters to query
				SqlCommand cmd = new(query, connection);
				cmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = id;


				// try to open connection with DB
				try
				{
					ContactModel foundContact;
					connection.Open();
					SqlDataReader reader = cmd.ExecuteReader();
					reader.Read();
					
					foundContact = (
						new ContactModel
						{
							ID = (int)reader[0],
							Name = (string)reader[1],
							Surname = (string)reader[2],
							Email = (string)reader[3],
							Password = (string)reader[4],
							Category = (string)reader[5],
							PhoneNumber = (string)reader[6],
							BirthDate = (DateTime)reader[7]
						});
					return foundContact;

					
				}
				catch (Exception e)
				{
					// print exeption if any
					Console.WriteLine(e.Message);
				}
			}
			return null;
		}
	}
}
