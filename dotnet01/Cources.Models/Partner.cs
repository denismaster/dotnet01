using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Partner : DomainObject
    {
        /// <summary>
        /// Ключевое поле
        /// </summary>
        public int PartnerId
        {
            get;
            set;
        }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? UserId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
        public User User { get; set; }
    }
}
