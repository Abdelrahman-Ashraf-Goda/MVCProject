using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication.DAL.Models;

namespace WebApplication.DAL.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) 
        {
        }

        
        public virtual DbSet<ProductModel> ProductModels { get; set; }
        public virtual DbSet<CategoryModel> CategoryModels { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel 
                {
                    Id = 1,
                    CategoryName = "Books",
                },
                new CategoryModel
                {
                    Id = 2,
                    CategoryName = "Cloting",
                }
                );

        }



    }
}
