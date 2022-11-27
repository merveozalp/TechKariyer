using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramewrok.Context
{
    public class MyContext:IdentityDbContext<AppUser,AppRole,string>
    {
        public MyContext()
        {

        }
        public MyContext(DbContextOptions<MyContext> options):base(options)
        {

        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TechKariyerDb;Trusted_Connection = True");
        }

    }
}
