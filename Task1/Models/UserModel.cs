using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading;
using Task1.Utils;

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
		public string passwordHash;
		public string PasswordHash
		{
			get { return passwordHash; }
			//override set to compute hash and dont store real password
			set
			{
				//initialize sha256
				SHA256 mySHA256 = SHA256.Create();
				//compute hash
				string hash= Utilities.GetHash(mySHA256, value);
				//store in the class field
				passwordHash = hash;
				
			}		 
		}
		public UserModel() { this.ID = -1; }
		//constructor from new user
		public UserModel(string Username, string passwordHash)
		{
			this.Username = Username;
			this.passwordHash = passwordHash;
			this.ID = Interlocked.Increment(ref nextID);
		}
		

	}
}
