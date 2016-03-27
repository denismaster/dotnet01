using System.Web.Mvc;

namespace Courses.ViewModels
{
    public class ProductForAddEditViewModel : ProductViewModel
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
