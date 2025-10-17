using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace Blazor_lab1.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {

        public DbSet<Blazor_lab1.Data.Book> Book { get; set; } = default!;

        public DbSet<Blazor_lab1.Data.Reader> Reader { get; set; } = default!;

        public DbSet<Blazor_lab1.Data.RentBook> RentBook { get; set; } = default!;

    }
}
