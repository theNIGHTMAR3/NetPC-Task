using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Threading;
using Task1.Utils;

namespace Task1.Models
{
	//model for contact
	public class ContactModel
	{
		[Required]
		public int ID { get; set; }
		[Required]
		public string Name{ get; set; }
		public string Surname { get; set; }
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[DataType(DataType.Password)]
		public string Password { get; set; }
		public string Category { get; set; }
		[Required]
		[Display(Name = "Phone number")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }
		[DataType(DataType.Date)]
		[Display(Name = "Birth date")]
		public DateTime BirthDate { get; set; }

		public ContactModel() { }

		ContactModel(string name, string surname, string email, string password, string category, string phoneNumber,DateTime birtdate)
		{
			this.Name = name;
			this.Surname = surname;
			this.Email = email;
			this.Password = password;
			this.Category = category;
			this.PhoneNumber = phoneNumber;
			this.BirthDate = birtdate;
		}
	}
}
