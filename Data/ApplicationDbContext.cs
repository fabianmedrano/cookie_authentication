using cookie_authentication.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace cookie_authentication.Data
{

    public class ApplicationDbContext : DbContext
    {

        public DbSet<User> Users { get; set; } = default!;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {
        }
   
     }


    
}
