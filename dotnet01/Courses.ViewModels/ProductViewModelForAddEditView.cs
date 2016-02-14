using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Courses.ViewModels
{
    public class ProductViewModelForAddEditView: ProductViewModel
    {
        /// <summary>
        /// Формируем список партнеров для передачи в представление
        /// </summary>
        public SelectList Partners
        {
            get;
            set;
        }
        /// <summary>
        /// Формируем список аккаунтов для передачи в представление
        /// </summary>
        public SelectList Accounts
        {
            get;
            set;
        }
    }
}
