using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class ProductWithCategorysViewModel: ProductViewModel
    {
        /// <summary>
        /// Категории продукта
        /// </summary>
        public List<CategoryViewModel> Categorys
        {
            get;
            set;
        }
    }
}
