using Microsoft.EntityFrameworkCore;
using ImageSearch.Models;

namespace ImageSearch.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options): base(options) 
        {
            Database.EnsureCreated();    
        }
        public DbSet<Admin> Admins;
        // public DbSet<Image> Images;
    }
}
