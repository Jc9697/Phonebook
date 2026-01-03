using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phonebook.Pages.PhonebookOperations
{
    public class IndexModel : PageModel
    {
        private readonly Phonebook.Data.PhonebookContext _context;

        public IndexModel(Phonebook.Data.PhonebookContext context)
        {
            _context = context;
        }

        public IList<Contact> Contact { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public async Task OnGetAsync()
        {
            var contacts = from c in _context.Contacts
                           select c;
            if (!string.IsNullOrEmpty(SearchString))
            {
                contacts = contacts.Where(p => p.Category.Contains(SearchString));
            }

            Contact = await contacts.ToListAsync();
        }
    }
}
