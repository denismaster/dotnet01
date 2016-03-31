using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required]
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int? UserId { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual User User { get; set; }

        public Partner()
        {
            Products = new  List<Product>();
            Categories = new  List<Category>();
        }
    }
}
