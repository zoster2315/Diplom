using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestingModule.Models;

namespace TestingModule.Data
{
    public class TestsContext : DbContext
    {
        public TestsContext(DbContextOptions<TestsContext> options) : base(options)
        {

        }

        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Rubric> Rubrics { get; set; }
        public DbSet<Duty> Duties { get; set; }
        public DbSet<UnitTest> UnitTests { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().ToTable("Subject");
            modelBuilder.Entity<Rubric>().ToTable("Rubric");
            modelBuilder.Entity<Duty>().ToTable("Duty");
            modelBuilder.Entity<UnitTest>().ToTable("UnitTest");
        }
    }
}
