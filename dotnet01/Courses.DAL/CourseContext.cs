using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Courses.Models;
namespace Courses.DAL
{
    public class CourseContext : DbContext
    {
        public CourseContext() :
            base("CoursesDatabase")
        { }

        public DbSet<Course> Course { get; set; }
    }
}
