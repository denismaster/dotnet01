using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class PartnerWithCategorysViewModel: PartnerViewModel
    {
        /// <summary>
        /// Категории партнера
        /// </summary>
        public List<CategoryViewModel> Categorys
        {
            get;
            set;
        }
    }
}
