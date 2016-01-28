using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Courses.Models;
namespace Courses.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() :
            base("AccountsDatabase")
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<EmailNewsletter> EmailNewsLetters { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Event> Events { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Partner> Partners { get; set; }

        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
