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

        #region инициализация репозиториев всех таблиц
        IPartnerRepository partnersRep = new PartnerRepository();
        IProductRepository productRep = new ProductRepository();
        IAccountRepository accRep = new AccountRepository();
        ICustomerRepository customerRep = new CustomerRepository();
        IProductRatingRepository productRatingRep = new ProductRatingRepository();
        ICategoryRepository categoryRep = new CategoryRepository();
        IAppointmentRepository appointmentRep = new AppointmentRepository();
        ICommentsRepository commentsRep = new CommentsRepository();
        IEmailTemplateRepoitory emailTemplateRep = new EmailTemplateRepository();
        IEventRepository eventRep = new EventRepository();
        IOrderRepository orderRep = new OrderRepository();
        IOrderItemRepository orderItemRep = new OrderItemRepository();
        IScheduleRepository scheduleRep = new ScheduleRepository();
        IEmailNewsletterRepository newsLetterRep = new EmailNewsLetterRepository();
        IEmailQueueRepository emailQueueRep = new EmailQueueRepository();
        #endregion
        #region методы для инициализации всех таблицы данными
      
        private void AddPartners()
        {
            DatabaseContext context = new DatabaseContext();
            
            partnersRep.ClearTable();

            Partner partner = new Partner
            {
                Name = "Васян",
                Address = "Ул.Пушкина дом Колотушкина",
                CreatedDate = new DateTime(2015, 11, 2),
                UpdatedDate = new DateTime(2015, 11, 4),
                Phone = "8 800 555 35 35",
                Email = "partner@mail.ru",
                Contact = "Звоните пишите",
                UserId = accRep.GetOnlyOne().Id,
                
            };
         
            partnersRep.Add(partner);
            partnersRep.SaveChanges();
        }
        private void AddProducts()
        {
            productRep.ClearTable();
            
            productRep.Add(new Product
            {
                Name = "Программирование Pascal",
                Location = "Севастополь",
                Description = "Лекции",
                CreatedDate = new DateTime(2015, 11, 2),
                UpdatedDate = new DateTime(2015, 11, 4),
                Active = true,
                Type = 1,
                PartnerId = partnersRep.GetOnlyOne().PartnerId,
                
                SeatsCount = 50,
                imagePath = "IMG.png",
                AssignedUserId = accRep.GetOnlyOne().Id,
                

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
                AssignedUserId = accRep.GetOnlyOne().Id,
                SeatsCount = 50,
                imagePath = "IMG.png",
 
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
               AssignedUserId = accRep.GetOnlyOne().Id,
               SeatsCount = 50,
               imagePath = "IMG.png",
               PartnerId = partnersRep.GetOnlyOne().PartnerId

           });
           productRep.SaveChanges();
       }
       private void AddUsers()
       {
           accRep.ClearTable();

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
           DatabaseContext db = new DatabaseContext();

          // db.Users.Attach(partner.User);
           accRep.Add(user);
           accRep.Add(user2);
           accRep.SaveChanges();





       }
       private void AddCustomers()
       {
           customerRep.ClearTable();
            
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
            customerRep.Add(customer);
            customerRep.SaveChanges();
            customerRep.AddFavouriteProduct(customerRep.GetOnlyOne().Id,productRep.GetOnlyOne().Id);
          


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
         
           customerRep.Add(customer2);
           customerRep.SaveChanges();

           customerRep.AddFavouriteProduct(customerRep.Get(1,1,o=>o.Login== "petushok").FirstOrDefault().Id, productRep.GetOnlyOne().Id);

            
       }
       private void AddProductRatings()
       {

           productRatingRep.ClearTable();
           ProductRating rating = new ProductRating();
            rating.ProductId = productRep.GetOnlyOne().Id;
            rating.CustomerId = customerRep.GetOnlyOne().Id;
           rating.Rate = 10;
           productRatingRep.Add(rating);
          
           productRatingRep.SaveChanges();
       }
       private void AddCategories()
       {
           categoryRep.ClearTable();
           Category category = new Category
           {
               Active = true,
               CreatedDate = new DateTime(2015, 12, 12),
               Description = "Программирование",
               UpdatedDate = new DateTime(2016, 1, 1),
               Name = "Программирование",
           };

            categoryRep.Add(category);
            categoryRep.SaveChanges();
            categoryRep.AddPartners(categoryRep.GetOnlyOne().CategoryId, partnersRep.GetOnlyOne().PartnerId);
            categoryRep.AddProducts(categoryRep.GetOnlyOne().CategoryId, productRep.GetOnlyOne().Id);
         
           Category category2 = new Category
           {
               Active = true,
               CreatedDate = new DateTime(2015, 12, 12),
               Description = "C#",
               UpdatedDate = new DateTime(2016, 1, 1),
               Name = "C# курсы",
               ParentCategory = category
       };
            categoryRep.Add(category2);
            categoryRep.SaveChanges();
            categoryRep.AddPartners(categoryRep.Get(1,1,o=>o.Description=="C#").FirstOrDefault().CategoryId, partnersRep.GetOnlyOne().PartnerId);
            categoryRep.AddProducts(categoryRep.Get(1, 1, o => o.Description == "C#").FirstOrDefault().CategoryId, productRep.GetOnlyOne().Id);
 
       }
       private void AddAppointments()
       {
           appointmentRep.ClearTable();
           Appointment app = new Appointment
           {
               Price = 500,
               ProductId = productRep.GetOnlyOne().Id,

           };
           Appointment app2 = new Appointment
           {
               Price = 322,
               ProductId = productRep.GetOnlyOne().Id,

           };
           appointmentRep.Add(app);
           appointmentRep.Add(app2);
           appointmentRep.SaveChanges();
       }
       private void AddComments()
       {
           commentsRep.ClearTable();
           Comment comment = new Comment
           {
               CreatedDate = DateTime.Now,
               Text = "Очень понравилось. круто, пацаны, ещё,жгите",
               UpdatedDate = DateTime.Now,
               CustomerId = customerRep.GetOnlyOne().Id,
               ProductId = productRep.GetOnlyOne().Id,
           };
           Comment comment2 = new Comment
           {
               CreatedDate = DateTime.Now,
               Text = "Плохо, жесть,ужас, просто ппц, я передумал",
               UpdatedDate = DateTime.Now,
               CustomerId = customerRep.GetOnlyOne().Id,
               ProductId = productRep.GetOnlyOne().Id,
           };
           commentsRep.Add(comment);
           commentsRep.Add(comment2);
           commentsRep.SaveChanges();
       }
       private void AddEmailTemplates()
       {
           emailTemplateRep.ClearTable();
           EmailTemplate temp = new EmailTemplate
           {
               Name = "Поздравление с днюхой",
           };
           EmailTemplate temp2 = new EmailTemplate
           {
               Name = "Подтверждение покупки",
           };
           emailTemplateRep.Add(temp);
           emailTemplateRep.Add(temp2);
           emailTemplateRep.SaveChanges();
       }
       private void AddEvents()
       {
           eventRep.ClearTable();
           Event event1 = new Event()
           {
               Changes = "Изменения",
               Entity = "Сущность изменения",
               CreatedDate = DateTime.Now,
               UserId = accRep.GetOnlyOne().Id, 
           };
           Event event2 = new Event()
           {
               Changes = "Изменения2",
               Entity = "Сущность изменения2",
               CreatedDate = DateTime.Now,
               UserId = accRep.GetOnlyOne().Id,
           };
           eventRep.Add(event1);
           eventRep.Add(event2);
           eventRep.SaveChanges();

       }
       private void AddOrders()
       {
           orderRep.ClearTable();
           Order order = new Order()
           {
               CreatedDate = DateTime.Now,
               CustomerId = customerRep.GetOnlyOne().Id,
               UpdateDate = DateTime.Now,
           };
           Order order2 = new Order()
           {
               CreatedDate = DateTime.Now,
               CustomerId = customerRep.GetOnlyOne().Id,
               UpdateDate = DateTime.Now,
           };
           orderRep.Add(order);
           orderRep.Add(order2);
           orderRep.SaveChanges();
       }
       private void AddOrderItems()
       {
           orderItemRep.ClearTable();
           OrderItem item = new OrderItem()
           {
               CreatedDate = DateTime.Now,
               SumTotal = 1489,
               UpdatedDate = DateTime.Now,
               AppointmentId = appointmentRep.GetOnlyOne().Id,
               OrderId = orderRep.GetOnlyOne().Id 
           };
           OrderItem item2 = new OrderItem()
           {
               CreatedDate = DateTime.Now,
               SumTotal = 322,
               UpdatedDate = DateTime.Now,
               AppointmentId = appointmentRep.GetOnlyOne().Id,
               OrderId = orderRep.GetOnlyOne().Id
           };
           orderItemRep.Add(item);
           orderItemRep.Add(item2);
           orderItemRep.SaveChanges();
       }
       private void AddSchedules()
       {
           scheduleRep.ClearTable();
            Schedule schedule = new Schedule()
            {
                StartDate = new DateTime(2016, 02, 02),
                EndDate = DateTime.Now,
                Text = "Расписание 1",
                AppointmentId = appointmentRep.GetOnlyOne().Id,
            };
           Schedule schedule2 = new Schedule()
           {
               StartDate = new DateTime(2016, 02, 01),
               EndDate = DateTime.Now,
               Text = "Дочернее расписание",
               AppointmentId = appointmentRep.GetOnlyOne().Id,
               ParentId = schedule.Id

           };
           scheduleRep.Add(schedule);
           scheduleRep.Add(schedule2);
           scheduleRep.SaveChanges();
       }
       private void AddEmailNewsLetters()
       {
           newsLetterRep.ClearTable();
           EmailNewsletter letter = new EmailNewsletter()
           {
               CreatedDate = DateTime.Now,
               Enabled = true,
               Name = "Письмо1",
               UdpatedDate = DateTime.Now,
               TemplateId = emailTemplateRep.GetOnlyOne().Id,
              
               
           };
           EmailNewsletter letter2 = new EmailNewsletter()
           {
               CreatedDate = DateTime.Now,
               Enabled = true,
               Name = "Письмо2",
               UdpatedDate = DateTime.Now,
               TemplateId = emailTemplateRep.GetOnlyOne().Id,
           };
           newsLetterRep.Add(letter);
           newsLetterRep.Add(letter2);
           newsLetterRep.SaveChanges();
       }
       private void AddEmailQueues()
       {
           emailQueueRep.ClearTable();
           EmailQueue queue = new EmailQueue()
           {
               CreatedDate = new DateTime(2015, 12, 12),
               CustomerId = customerRep.GetOnlyOne().Id,
               NewsletterId = newsLetterRep.GetOnlyOne().Id,
               Status = 1,
               UpdatedDate = DateTime.Now
           };
           EmailQueue queue2 = new EmailQueue()
           {
               CreatedDate = new DateTime(2015, 11, 12),
               CustomerId = customerRep.GetOnlyOne().Id,
               NewsletterId = newsLetterRep.GetOnlyOne().Id,
               Status = 1,
               UpdatedDate = DateTime.Now
           };
           emailQueueRep.Add(queue);
           emailQueueRep.Add(queue2);
           emailQueueRep.SaveChanges();
       }
       #endregion
       protected override void Seed(DatabaseContext db)
       {
           AddUsers();
            AddPartners();
           AddProducts();
            AddCustomers();
            AddProductRatings();
            AddCategories();
             AddAppointments();
          AddComments();
               AddEmailTemplates();
               AddEvents();
             AddOrders();
              AddOrderItems();
             AddSchedules();
            AddEmailNewsLetters();
              AddEmailQueues();
        }
    }
}
