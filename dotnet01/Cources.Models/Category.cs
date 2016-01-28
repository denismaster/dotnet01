using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Category : DomainObject
    {
        /// <summary>
        /// Ключевое поле
        /// </summary>
        public int CategoryId
        {
            get;
            set;
        }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int? ParentId { get; set; }
        public List<Partner> Partners { get; set; }
        public List<Product> Products { get; set; }
        [ForeignKey("ParentId")]
        public Category _Category { get; set; }
    }
}
