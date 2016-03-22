using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories
{
    public interface ICustomerRepository: IRepository<Customer>, IDisposable
    {
 
        void AddFavouriteProduct(int customerId, int productId);
        void AddFavouriteProduct(Customer customer, Product product);
    }
}
