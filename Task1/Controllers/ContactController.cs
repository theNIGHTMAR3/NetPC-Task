using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Task1.Models;
using Task1.Utils;

namespace Task1.Controllers
{
	public class ContactController : Controller
	{
		readonly ContactsDAO contactsDAO = new();
		public IActionResult Index()
		{
		
			// on index page show all contacts from DB
			return View(contactsDAO.FindAllContacts());
		}
		// creation of new contact
		public IActionResult Create()
		{
			return View();
		}
		// summary of created contact
		public IActionResult Summary(ContactModel contact)
		{
			// if there does not exist contact with given adress
			if(!contactsDAO.CheckIfEmailExists(contact.Email) && Utilities.IsPasswordStrongEnough(contact.Password))
			{
				// add contact to the DB
				contactsDAO.AddNewContact(contact);
				// show summary of added contact
				return View("Summary",contact);
			}
			// when failed to create a contact
			return View("ContactFailed",contact);
		}
		
		// show single contact details
		public IActionResult Details(int id)
		{
			ContactModel singleContact = contactsDAO.FindContactByID(id);
			return View("ShowDetails", singleContact);
		}

		// delete choosen contact
		public IActionResult Delete(int id)
		{
			contactsDAO.DeleteContactByID(id);
			return View("Index", contactsDAO.FindAllContacts());
		}

		// edits choosen contact
		public IActionResult Edit(int id)
		{
			ContactModel singleContact = contactsDAO.FindContactByID(id);
			return View("EditContact", singleContact);
		}

		// processes edit operation
		public IActionResult ProcessEdit(ContactModel contact)
		{
			contactsDAO.UpdateContact(contact);
			return View("Index", contactsDAO.FindAllContacts());
		}

	}
}
