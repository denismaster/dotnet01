using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class CourseCollectionViewModel
    {
        public IEnumerable<CourseViewModel> Courses
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
