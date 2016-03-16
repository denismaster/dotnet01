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
            base("CoursesDatabase")
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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            

            modelBuilder.Entity<Product>()
             .HasMany(c => c.Appointments)
              .WithRequired(o => o.Product).
              HasForeignKey(o => o.ProductId);

            modelBuilder.Entity<Appointment>()
                .HasMany(o => o.Schedules)
                .WithRequired(o => o.Appointment)
                .HasForeignKey(o => o.AppointmentId);

            modelBuilder.Entity<Appointment>()
                .HasMany(o => o.OrderItems)
                .WithRequired(o => o.Appointment)
                .HasForeignKey(o => o.AppointmentId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithRequired(o => o.Order)
                .HasForeignKey(o => o.OrderId);

            modelBuilder.Entity<Customer>()
                .HasMany(o => o.Orders)
                .WithRequired(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasMany(o => o.Comments)
                .WithRequired(o => o.Customer)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Product>()
                .HasMany(o => o.Comments)
                .WithRequired(o => o.Product)
                .HasForeignKey(o => o.ProductId);

            modelBuilder.Entity<Partner>()
                .HasMany(o => o.Products)
                .WithRequired(o => o.Partner)
                .HasForeignKey(o => o.PartnerId);

            modelBuilder.Entity<User>()
                .HasMany(o => o.Products)
                .WithOptional(o => o.User)
                .HasForeignKey(o => o.AssignedUserId);

            modelBuilder.Entity<EmailNewsletter>()
                .HasMany(o => o.EmailQueues)
                .WithOptional(o => o.EmailNewsLetter)
                .HasForeignKey(o => o.NewsletterId);

            modelBuilder.Entity<Customer>()
             .HasMany(o => o.EmailQueues)
             .WithRequired(o => o.Customer)
             .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<EmailTemplate>()
                .HasMany(o => o.EmailNewsLetters)
                .WithOptional(o => o.EmailTemplate)
                .HasForeignKey(o => o.TemplateId);

            modelBuilder.Entity<User>()
               .HasMany(o => o.Partners)
               .WithOptional(o => o.User)
               .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<User>()
               .HasMany(o => o.Events)
               .WithRequired(o => o.User)
               .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<Product>()
                 .HasMany(o => o.CustomersWithFavouriteProducts)
                 .WithMany(o => o.FavouriteProducts)
                 .Map(m =>
                 {
                     m.ToTable("FavouriteProducts");
                     m.MapLeftKey("ProductId");
                     m.MapRightKey("CustomerId");

                 });

            modelBuilder.Entity<ProductRating>().HasKey(e => new { e.ProductId, e.CustomerId });

            modelBuilder.Entity<Product>()
                .HasMany(o => o.ProductRatings)
                .WithRequired(o => o.Product)
                .HasForeignKey(o => o.ProductId);

            modelBuilder.Entity<Customer>()
               .HasMany(o => o.ProductRatings)
               .WithRequired(o => o.Customer)
               .HasForeignKey(o => o.CustomerId);
               
            modelBuilder.Entity<Schedule>()
                .HasOptional(o => o._Schedule);

            modelBuilder.Entity<Category>()
                .HasOptional(o => o.ParentCategory);
        }

    }
}
