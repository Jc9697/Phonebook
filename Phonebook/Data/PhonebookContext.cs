using Microsoft.EntityFrameworkCore;
namespace Phonebook.Data
{
    public class PhonebookContext : DbContext
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
