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

        public DbSet<ImageSearch.Models.Admin>? Admins { get; set; }
        // public DbSet<Image> Images;
    }
}
