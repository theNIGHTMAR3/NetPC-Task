using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Task1.Models;


namespace Task1.Utils
{
	// utility class for some helpful static methods
	public static class Utilities
	{
		// cheks if given password is strong enough
		public static bool IsPasswordStrongEnough(string password)
		{
			// is long enough
			if (password.Length < 7)
			{
				return false;
			}
			// contains at leat 1 Capital letter
			if (!password.Any(char.IsUpper))
			{
				return false;
			}
			// contains at leat 1 digit
			if (!password.Any(char.IsDigit))
			{
				return false;
			}
			// does not contain white space	
			if (password.Contains(" "))
			{
				return false;
			}
			return true;
		}

		// took these methods from microsoft documentations
		// create hash from string
		public static string GetHash(HashAlgorithm hashAlgorithm, string input)
		{
			// Convert the input string to a byte array and compute the hash.
			byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes
			// and create a string.
			var sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data
			// and format each one as a hexadecimal string.
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string.
			return sBuilder.ToString();
		}

		// Verify a hash against a string.
		public static bool VerifyHash(HashAlgorithm hashAlgorithm, string input, string hash)
		{
			// Hash the input.
			var hashOfInput = GetHash(hashAlgorithm, input);

			// Create a StringComparer an compare the hashes.
			StringComparer comparer = StringComparer.OrdinalIgnoreCase;

			return comparer.Compare(hashOfInput, hash) == 0;
		}
	}
}