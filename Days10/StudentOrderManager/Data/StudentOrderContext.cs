using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace StudentOrderManager.Data
{
    public class StudentOrderContext : DbContext
    {
        public StudentOrderContext(DbContextOptions<StudentOrderContext> options) : base(options) { }
        public StudentOrderContext() { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                optionsBuilder.UseMySql(connStr, ServerVersion.AutoDetect(connStr));
            }
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Order> Orders { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string Customer { get; set; }
        public decimal Total { get; set; }
    }
} 