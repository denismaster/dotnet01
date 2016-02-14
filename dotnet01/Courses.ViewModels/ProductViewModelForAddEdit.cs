using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class ProductViewModelForAddEdit
    {
        public ProductViewModel product;

        public IEnumerable<PartnerViewModel> Partners
        {
            get;
            set;
        }

        public IEnumerable<AccountViewModel> Accounts
        {
            get;
            set;
        }
    }
}
