using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Phonebook.Models;

namespace Phonebook.Pages.PhonebookOperations
{
    [Authorize(Policy = "EmailConfirmedOnly")]
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
                _context.Contacts.Add(Contact);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                var existingContactEmail = _context.Contacts
                    .FirstOrDefault(c => c.Email == Contact.Email);
                var existingContactNumber = _context.Contacts
                    .FirstOrDefault(c => c.PhoneNumber == Contact.PhoneNumber);
                if (existingContactEmail != null)
                {
                    ModelState.AddModelError("Contact.Email", "A contact with this email already exists.");
                    return Page();
                }
                if (existingContactNumber != null)
                {
                    ModelState.AddModelError("Contact.PhoneNumber", "A contact with this number already exists.");
                    return Page();
                }
                return Page();
            }

        }
    }
}
