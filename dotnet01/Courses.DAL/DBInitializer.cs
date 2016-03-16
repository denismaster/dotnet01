using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Models;
using Courses.Models.Repositories;

namespace Courses.DAL



{
    public class DBInitializer : DropCreateDatabaseAlways<DatabaseContext>
    {


        IPartnerRepository partnersRep = new PartnerRepository();
        IProductRepository productRep = new ProductRepository();
        IAccountRepository accRep = new AccountRepository();
        ICustomerRepository customerRep = new CustomerRepository();
        IProductRatingRepository productRatingRep = new ProductRatingRepository();
        ICategoryRepository categoryRep = new CategoryRepository();

        private void AddPartners()
        {


            Partner partner = new Partner
            {
                Name = "Васян",
                Address = "Ул.Пушкина дом Колотушкина",
                CreatedDate = new DateTime(2015, 11, 2),
                UpdatedDate = new DateTime(2015, 11, 4),
                Phone = "8 800 555 35 35",
                Email = "partner@mail.ru",
                Contact = "Звоните пишите",
                UserId = null
            };

            partnersRep.Add(partner);
            partnersRep.SaveChanges();
        }
        private void AddProducts()
        {

            productRep.Add(new Product
            {
                Name = "Программирование Pascal",
                Location = "Севастополь",
                Description = "Лекции",
                CreatedDate = new DateTime(2015, 11, 2),
                UpdatedDate = new DateTime(2015, 11, 4),
                Active = true,
                Type = 1,
                Partner = partnersRep.GetOnlyOne(),
                AssignedUserId = null,
                SeatsCount = 50,
                imagePath = "IMG.png"

            });
            productRep.Add(new Product
            {
                Name = "Программирование Java",
                Location = "Сев",
                Description = "Курсы",
                CreatedDate = new DateTime(2015, 11, 2),
                UpdatedDate = new DateTime(2015, 11, 4),
                Active = true,
                Type = 1,
                Partner = partnersRep.GetOnlyOne(),
                AssignedUserId = null,
                SeatsCount = 50,
                imagePath = "IMG.png"

            });
            productRep.Add(new Product
            {
                Name = "Дотнет",
                Location = "Сев",
                Description = "Курсы",
                CreatedDate = new DateTime(2015, 11, 2),
                UpdatedDate = new DateTime(2015, 11, 4),
                Active = true,
                Type = 1,
                Partner = partnersRep.GetOnlyOne(),
                AssignedUserId = null,
                SeatsCount = 50,
                imagePath = "IMG.png"

            });
            productRep.SaveChanges();
        }
        private void AddUsers()
        {

            User user = new User
            {
                AuthKey = "1ADSDFS1231SL",
                CreatedDate = new DateTime(2016, 10, 10),
                Email = "email@mail.ru",
                FirstName = "Кирилл",
                LastName = "Сухоруких",
                Login = "KirillSuhorukih",
                PasswordHash = "asdasdasfhj1jg3123123123ghdsfdghkg123",
                Role = "Admin",
                Status = 1,
                UpdatedDate = new DateTime(2016, 12, 12)
            };
            User user2 = new User
            {
                AuthKey = "1dfsdfsfsdfsdfsdfdsfdsf",
                CreatedDate = new DateTime(2016, 4, 3),
                Email = "mailmail@mail.ru",
                FirstName = "Иван",
                LastName = "Иванов",
                Login = "IvanIvanov",
                PasswordHash = "afghasjgalkjgasjlglj123",
                Role = "Manager",
                Status = 21,
                UpdatedDate = new DateTime(2016, 11, 11)
            };

            accRep.Add(user);
            accRep.Add(user2);
            accRep.SaveChanges();
        }
        private void AddCustomers()
        {

            Customer customer = new Customer
            {
                AuthKey = "1ADSDFS1231SL",
                CreatedDate = new DateTime(2016, 10, 10),
                Email = "email@mail.ru",
                FirstName = "Кирилл",
                LastName = "Сухоруких",
                Login = "KirillSuhorukih",
                PasswordHash = "asdasdasfhj1jg3123123123ghdsfdghkg123",
                Address = "ул. Блаблабла",
                BirthDate = new DateTime(1990, 10, 5),
                City = "City",
                Gender = true,
                PasswordResetToken = "12312arfad",
                Phone = "1488 123 123",
                Role = 1,
                Status = 1,
                UpdatedDate = new DateTime(2016, 12, 12)
            };
            Customer customer2 = new Customer
            {
                AuthKey = "1asdfasdfL",
                CreatedDate = new DateTime(2016, 4, 4),
                Email = "adfds@asdfasd.ru",
                FirstName = "Petya",
                LastName = "Petya",
                Login = "petushok",
                PasswordHash = "aasgasggafgasgasg",
                Address = "ул. adfadf",
                BirthDate = new DateTime(1989, 10, 5),
                City = "adfd",
                Gender = true,
                PasswordResetToken = "1asdfasdfasdfa",
                Phone = "123123123",
                Role = 1,
                Status = 1,
                UpdatedDate = new DateTime(2016, 12, 5)
            };
            customerRep.Add(customer);
            customerRep.Add(customer2);
            customerRep.SaveChanges();
        }
        private void AddProductRatings()
        {


            ProductRating rating = new ProductRating();

            rating.Product = productRep.GetOnlyOne();
            rating.Customer = customerRep.GetOnlyOne();

            rating.Rate = 10;




            productRatingRep.Add(rating);
            //productRatingRep.Add(rating2);
            productRatingRep.SaveChanges();
        }
        IEnumerable<Category> categories;
        private void AddCategories()
        {
            Category category = new Category
            {
                Active = true,
                CreatedDate = new DateTime(2015, 12, 12),
                Description = "Программирование",
                UpdatedDate = new DateTime(2016, 1, 1),
                Name = "Программирование",
            };
            Category category2 = new Category
            {
                Active = true,
                CreatedDate = new DateTime(2015, 12, 12),
                Description = "C#",
                UpdatedDate = new DateTime(2016, 1, 1),
                Name = "C# курсы",
                ParentCategory = category
        };
            categoryRep.Add(category);
            categoryRep.Add(category2);
            categoryRep.SaveChanges();
           
            
        }
    
 
        protected override void Seed(DatabaseContext db)
        {
            AddPartners();
            AddProducts();
            AddUsers();
            AddCustomers();
            AddProductRatings();
            AddCategories();
        } 
    }
}
