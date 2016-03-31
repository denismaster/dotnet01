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
        public Category()
        {
            Partners = new List<Partner>();
            Products = new List<Product>();
        }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Active { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public string Description { get; set; }
        public virtual Category ParentCategory { get; set; }

        //инициализировать вручную данный айди
        public int? ParentCategoryId { get; set; }
    }
}
