using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class EmailQueue : DomainObject
    {   /// <summary>
        /// Ключевое поле
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        public int? NewsletterId { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int Status { get; set; }
        public virtual EmailNewsletter EmailNewsLetter { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
