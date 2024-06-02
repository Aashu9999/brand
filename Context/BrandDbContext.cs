using BrandWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BrandWebApi.Context
{
    public class BrandDbContext:DbContext
    {
        public BrandDbContext(DbContextOptions<BrandDbContext> options) : base(options) 
        { 

        }

       public DbSet<Brand> brands {  get; set; }
    }
}
