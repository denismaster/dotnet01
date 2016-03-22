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
    class CustomerRepository:RepositoryBase<Customer>, ICustomerRepository
    {
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
