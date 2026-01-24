using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Phonebook.Models;

namespace Phonebook.Pages.PhonebookOperations
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly Phonebook.Data.PhonebookContext _context;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public IndexModel(Phonebook.Data.PhonebookContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get; set; } = default!;

        public async Task OnGetAsync()
        {
            Contact = await _context.Contacts.ToListAsync();
        }

        public IActionResult OnGetFilter()
        {
            // Filter your data using LINQ
            var filteredData = _context.Contacts
                .Where(c => string.IsNullOrEmpty(SearchString) ||
                    c.Name.Contains(SearchString) ||
                    c.Email.Contains(SearchString) ||
                    c.PhoneNumber.Contains(SearchString) ||
                    c.Category.Contains(SearchString))
                .ToList();

            // Returns ONLY the markup for the list/table
            return Partial("Shared/_ResultsPartial", filteredData);
        }
    }
}
