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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=phonebook.db");
        }

        public DbSet<Phonebook.Models.Contact> Contacts { get; set; }
    }
}
