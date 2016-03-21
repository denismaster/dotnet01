using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Courses.ViewModels
{
    public class CategoryViewModelForAddEditView: CategoryViewModel
    {
        /// <summary>
        /// Формируем список  категорий для передачи их в представление 
        /// и возможности выбора родительской категории
        /// </summary>
        public SelectList Categorys
        {
            get;
            set;
        }
    }
}
