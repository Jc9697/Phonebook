using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Pages.PhonebookOperations
{
    [Authorize(Policy = "EmailConfirmedOnly")]
    public class DeleteModel : PageModel
    {
        private readonly Phonebook.Data.PhonebookContext _context;

        public DeleteModel(Phonebook.Data.PhonebookContext context)
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

            if (contact is not null)
            {
                Contact = contact;

                return Page();
            }

            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = await _context.Contacts.FindAsync(id);
            if (contact != null)
            {
                Contact = contact;
                _context.Contacts.Remove(Contact);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
