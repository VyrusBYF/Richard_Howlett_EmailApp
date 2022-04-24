using Microsoft.EntityFrameworkCore;
using EmailService;

namespace EmailApp
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Email> Emails { get; set; }

    }
}
