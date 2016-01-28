using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Event : DomainObject
    {
        /// <summary>
        /// Ключевое поле
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        public string Entity { get; set; }
        public string Changes { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public User User { get; set; }
    }
}
