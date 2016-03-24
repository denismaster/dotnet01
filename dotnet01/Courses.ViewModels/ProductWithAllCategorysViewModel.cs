using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Courses.ViewModels
{
    public class ProductWithAllCategorysViewModel: ProductWithCategorysViewModel
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
