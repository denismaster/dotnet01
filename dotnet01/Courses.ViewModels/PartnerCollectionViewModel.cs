using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class PartnerCollectionViewModel
    {
        public IEnumerable<PartnerViewModel> Partners
        {
            get;
            set;
        }
        public PageInfo PageInfo
        {
            get;
            set;
        }
    }
}
