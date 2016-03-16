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

   
    }
}
