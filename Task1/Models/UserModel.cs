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
		[Required]
		public string Username { get; set; }

		//change displaying PasswordHash to Password
		[Required]
		[Display(Name = "Password")]
		public string PasswordHash { get; set; }

		public UserModel() {}

		//constructor from new user
		public UserModel(int ID,string Username, string passwordHash)
		{
			this.ID = ID;
			this.Username = Username;
			this.PasswordHash = passwordHash;		
		}
	}
}
