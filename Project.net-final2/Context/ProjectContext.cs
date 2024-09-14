using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project.net_final2.Models;

namespace Project.net_final2.Context
{
    public class ProjectContext : IdentityDbContext<IdentityUser>
    {
        public ProjectContext(DbContextOptions<ProjectContext> options): base(options)
        { 

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=project.net-2;Trusted_Connection=True;Encrypt=false");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var _Categories = new List<Category>
            {
                new Category { Id = 1, Name = "Cat1", Description="description of Category num 1" },
                new Category { Id = 2, Name = "Cat2",  Description="description of Category num 2"  },
                new Category { Id = 3, Name = "Cat3",  Description="description of Category num 3"  }

            };

            modelBuilder.Entity<Category>().HasData(_Categories);
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Book> Books { get; set; }
    }
}
