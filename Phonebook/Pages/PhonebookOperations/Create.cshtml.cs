using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook.Pages.PhonebookOperations
{
    public class CreateModel : PageModel
    {
        private readonly Phonebook.Data.PhonebookContext _context;

        public CreateModel(Phonebook.Data.PhonebookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Contact Contact { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is invalid");
                return Page();
            }
            try
            {
                var existingContact = _context.Contacts
                    .FirstOrDefault(c => c.Email == Contact.Email);
                if (existingContact != null)
                {
                    ModelState.AddModelError("Contact.Email", "A contact with this email already exists.");
                    return Page();
                }
                else
                {
                    _context.Contacts.Add(Contact);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("./Index");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking for existing contact: {ex.Message}");
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return RedirectToPage("./Index");
            }

        }
    }
}
