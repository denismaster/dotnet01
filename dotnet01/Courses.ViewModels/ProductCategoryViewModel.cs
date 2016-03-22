using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Courses.ViewModels
{
    public class ProductCategoryViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название мероприятия")]
        public string Name { get; set; }

        [DisplayName("Категории (направления) курса")]
        public List<CategoryViewModel> SelectedCategorys
        {
            get;
            set;
        }

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
