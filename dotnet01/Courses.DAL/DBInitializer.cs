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
    public class DBInitializer 
    {

        
        IPartnerRepository partnersRep = new PartnerRepository();
        IProductRepository productRep = new ProductRepository();
        IAccountRepository accRep = new AccountRepository();
        ICustomerRepository customerRep = new CustomerRepository();
        IProductRatingRepository productRatingRep = new ProductRatingRepository();
        

        private bool isExistAnyEntity<T>(IRepository<T> rep) where T : DomainObject
        {
            if (rep.Count(e => true) > 0) return true;
            else return false;
        }
        private void AddPartners()
        {
            if (isExistAnyEntity(partnersRep)) return;

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
            if (isExistAnyEntity(productRep)) return;
            productRep.Add(new Product
            {
                Name = "Программирование C#",
                Location = "Севастополь",
                Description = "Лекции",
                CreatedDate = new DateTime(2015, 11, 2),
                UpdatedDate = new DateTime(2015, 11, 4),
                Active = true,
                Type = 1,
                PartnerId = partnersRep.GetOnlyOne().PartnerId,
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
                PartnerId = partnersRep.GetOnlyOne().PartnerId,
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
                PartnerId = partnersRep.GetOnlyOne().PartnerId,
                AssignedUserId = null,
                SeatsCount = 50,
                imagePath = "IMG.png"

            });
            productRep.SaveChanges();
        }
        private void AddUsers()
        {
            if (isExistAnyEntity(accRep)) return;
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
                Status =21,
                UpdatedDate = new DateTime(2016, 11, 11)
            };

            accRep.Add(user);
            accRep.Add(user2);
            accRep.SaveChanges();
        }
        private void AddCustomers()
        {
            if (isExistAnyEntity(customerRep)) return;
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
                UpdatedDate = new DateTime(2016, 12,5)
            };
            customerRep.Add(customer);
            customerRep.Add(customer2);
            customerRep.SaveChanges();
        }
      /*  private void AddProductRatings()
        {
            if (isExistAnyEntity(productRatingRep)) return;

            ProductRating rating = new ProductRating();
            rating.ProductId = productRep.GetOnlyOne().Id;
            rating.CustomerId = customerRep.GetOnlyOne().Id;
            rating.Rate = 10;
           

         
       
            productRatingRep.Add(rating);
            //productRatingRep.Add(rating2);
            productRatingRep.SaveChanges();
        }
    */
        public void Init()
        {
            AddPartners();
            AddProducts();
            AddUsers();
            AddCustomers();
            //AddProductRatings();
        }
    }
}
