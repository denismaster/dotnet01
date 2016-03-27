using System.Collections.Generic;

namespace Courses.ViewModels
{
    public class OrderCollectionViewModel
    {
        public IEnumerable<OrderViewModel> Orders
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
