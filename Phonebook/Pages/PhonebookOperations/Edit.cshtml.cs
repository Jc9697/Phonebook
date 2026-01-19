using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Pages.PhonebookOperations
{
    [Authorize(Policy = "EmailConfirmedOnly")]
    public class EditModel : PageModel
    {
        private readonly Phonebook.Data.PhonebookContext _context;

        public EditModel(Phonebook.Data.PhonebookContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contact Contact { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FirstOrDefaultAsync(m => m.Id == id);
            if (contact == null)
            {
                return NotFound();
            }
            Contact = contact;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Contact).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ContactExists(Contact.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (DbUpdateException ex)
            {
                var existingContactEmail = _context.Contacts
                    .FirstOrDefault(c => c.Email == Contact.Email && c.Id != Contact.Id);
                var existingContactNumber = _context.Contacts
                    .FirstOrDefault(c => c.PhoneNumber == Contact.PhoneNumber && c.Id != Contact.Id);
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
                throw;
            }
            return RedirectToPage("./Index");
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
