using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace Phonebook.Data
{
    public class PhonebookContext : IdentityDbContext
    {

        public PhonebookContext(DbContextOptions<PhonebookContext> options)
        : base(options)
        {
        }

        public DbSet<Phonebook.Models.Contact> Contacts { get; set; }
    }
}
