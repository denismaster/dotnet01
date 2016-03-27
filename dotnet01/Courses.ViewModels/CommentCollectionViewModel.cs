using System.Collections.Generic;

namespace Courses.ViewModels
{
    public class CommentCollectionViewModel
    {
        public IEnumerable<CommentViewModel> Comments
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
