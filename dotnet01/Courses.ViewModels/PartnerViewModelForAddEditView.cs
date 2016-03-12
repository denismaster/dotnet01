using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Courses.ViewModels
{
    public class PartnerViewModelForAddEditView: PartnerViewModel
    {
        /// <summary>
        /// Формируем список пользователе для передачи в представление
        /// </summary>
        public SelectList Managers
        {
            get;
            set;
        }
    }
}
