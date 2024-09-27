using Exercise_21.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Exercise_21.Data
{
    public class PhoneBookContext : IdentityDbContext<User>
    {
        public PhoneBookContext(DbContextOptions<PhoneBookContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
    }
}
