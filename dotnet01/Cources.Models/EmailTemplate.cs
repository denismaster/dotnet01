using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class EmailTemplate : DomainObject
    {
        /// <summary>
        /// Ключевое поле
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        public string Name { get; set; }
        public virtual ICollection<EmailNewsletter> EmailNewsLetters { get; set; }
        public EmailTemplate()
        {
            EmailNewsLetters = new List<EmailNewsletter>();
        }
    }
}
