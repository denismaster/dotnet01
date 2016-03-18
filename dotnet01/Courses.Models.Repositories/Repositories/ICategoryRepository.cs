using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models.Repositories
{
   public interface ICategoryRepository: IRepository<Category>
    {

        Category GetOnlyOne();
        void AddPartners(int categoryId, int partnerId);
        void AddProducts(int categoryId, int productId);
   
    }
}
