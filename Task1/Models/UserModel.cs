using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading;

namespace Task1.Models
{
	//model for user
	public class UserModel
	{
		//basic info about user
		public int ID { get; set; }
		static int nextID;
		public string Username { get; set; }

		//change displaying PasswordHash to Password
		[Display(Name = "Password")]
		public string PasswordHash
		{
			get { return PasswordHash; }
			//override set to compute hash and dont store real password
			set
			{
				string pass = value;
				SHA256 mySHA256 = SHA256.Create();
				
				string passwordHash= Utilities.GetHash(mySHA256, pass);
				this.PasswordHash = passwordHash;
				
			}		 
		}

		//constructor from new user
		public UserModel(string Username, string PasswordHash)
		{
			this.Username = Username;
			this.PasswordHash = PasswordHash;
			this.ID = Interlocked.Increment(ref nextID);
		}
		public UserModel(){}

	}
}
