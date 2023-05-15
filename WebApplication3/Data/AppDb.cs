using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Data
{
    //if we want to add app role can add it here next to app user
    //After that we Add-Migration Identity throw package console
    public class AppDb : IdentityDbContext<AppUser>
    {
        public AppDb(DbContextOptions<AppDb> options) : base(options)
        {

        }
        public DbSet<Race> Races { get; set; }
        public DbSet<Club> Clubs { get; set; }  
        public DbSet<Address> Addresss { get; set; }

    }
}
