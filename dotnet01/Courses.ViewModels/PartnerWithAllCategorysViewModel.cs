using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class PartnerWithAllCategorysViewModel: PartnerWithCategorysViewModel
    {
        /// <summary>
        /// Формируем список всех категорий для передачи в представление
        /// </summary>
        public List<CategoryViewModel> AllCategorys
        {
            get;
            set;
        }
    }
}
