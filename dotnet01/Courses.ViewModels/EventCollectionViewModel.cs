using System.Collections.Generic;

namespace Courses.ViewModels
{
    public class EventCollectionViewModel
    {
        public IEnumerable<EventViewModel> Events
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
