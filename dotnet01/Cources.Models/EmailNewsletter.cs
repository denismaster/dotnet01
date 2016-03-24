using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class EmailNewsletter : DomainObject
    {
        /// <summary>
        /// Ключевое поле
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        public int? TemplateId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UdpatedDate { get; set; }
        public string Name { get; set; }
        public bool Enabled { get; set; }
        public virtual ICollection<EmailQueue> EmailQueues { get; set; }
        public EmailTemplate EmailTemplate { get; set; }
        public EmailNewsletter()
        {
            EmailQueues = new List<EmailQueue>();
        }
        
    }
}
