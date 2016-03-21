using Courses.Models;
using Courses.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.DAL
{
    class CustomerRepository: ICustomerRepository
    {
        DatabaseContext context = new DatabaseContext();

        public IEnumerable<Customer> Get()
        {
            return context.Customers.AsEnumerable();
        }
        public IEnumerable<Customer> Get(int page, int pageSize, Func<Customer, bool> expression, SortFilter sortFilter)
        {
            return null;
        }
        public IEnumerable<Customer> Get(int page, int pageSize, Func<Customer, bool> expression)
        {
            return context.Customers.Where(expression).Skip((page - 1) * pageSize).Take(pageSize).AsEnumerable();
        }

        public Customer Get(int id)
        {
            return context.Customers.Find(id);
        }

        public void Add(Customer entity)
        {
            context.Customers.Add(entity);
        }

        public void Update(Customer entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }

        public void Delete(Customer entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
        }


        public void SaveChanges()
        {
            context.SaveChanges();
        }


        public int Count(Func<Customer, bool> expression)
        {
            return context.Customers.Where(expression).Count();
        }

        public void Dispose()
        {
            context.Dispose();
        }
        public Customer GetOnlyOne()
        {
            return context.Customers.FirstOrDefault();
        }
        public void ClearTable()
        {
            context.Customers.RemoveRange(Get());
        }

        public void AddFavouriteProduct(int customerId, int productId)
        {

              using (DatabaseContext context = new DatabaseContext()) { 
       
                    Customer customer = new Customer { Id = customerId };
                    context.Customers.Add(customer);
                    context.Customers.Attach(customer);

                    Product product = new Product { Id = productId };
                    context.Products.Add(product);
                    context.Products.Attach(product);

                    if (customer.FavouriteProducts==null)
                    customer.FavouriteProducts = new List<Product>();

                    customer.FavouriteProducts.Add(product);
                    context.SaveChanges();
           }
        }
        public void AddFavouriteProduct(Customer customer,Product product)
        {
            throw new NotImplementedException();
        }
    }
}
