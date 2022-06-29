using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
			// if user is not logged in
			if (HttpContext.Session.GetString("user")==null)
			{
				// redirect to main page
				return View("../Home/Index");
			}

			return View();
		}
		// summary of created contact
		public IActionResult Summary(ContactModel contact)
		{
			// if user is not logged in
			if (HttpContext.Session.GetString("user") == null)
			{
				// redirect to main page
				return View("../Home/Index");
			}
			// if there does not exist contact with given adress
			if (!contactsDAO.CheckIfEmailExists(contact.Email) && Utilities.IsPasswordStrongEnough(contact.Password))
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
			// if user is not logged in
			if (HttpContext.Session.GetString("user") == null)
			{
				// redirect to main page
				return View("../Home/Index");
			}

			ContactModel singleContact = contactsDAO.FindContactByID(id);
			return View("ShowDetails", singleContact);
		}

		// delete choosen contact
		public IActionResult Delete(int id)
		{
			// if user is not logged in
			if (HttpContext.Session.GetString("user") == null)
			{
				// redirect to main page
				return View("../Home/Index");
			}

			contactsDAO.DeleteContactByID(id);
			return View("Index", contactsDAO.FindAllContacts());
		}

		// edits choosen contact
		public IActionResult Edit(int id)
		{
			// if user is not logged in
			if (HttpContext.Session.GetString("user") == null)
			{
				// redirect to main page
				return View("../Home/Index");
			}

			ContactModel singleContact = contactsDAO.FindContactByID(id);
			return View("EditContact", singleContact);
		}

		// processes edit operation
		public IActionResult ProcessEdit(ContactModel contact)
		{
			// if user is not logged in
			if (HttpContext.Session.GetString("user") == null)
			{
				// redirect to main page
				return View("../Home/Index");
			}

			contactsDAO.UpdateContact(contact);
			return View("Index", contactsDAO.FindAllContacts());
		}

	}
}
