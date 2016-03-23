using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class User : DomainObject
    {
        public User()
        {
            Products = new List<Product>();
            Partners = new List<Partner>();
            Events = new List<Event>();
            Role = "Default";
        }
        /// <summary>
        /// Ключевое поле
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AuthKey { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public byte Status { get; set; }
        public string ProviderName { get; set; }
        public DateTime CreatedDate
        {
            get;
            set;
        }
        public DateTime UpdatedDate
        {
            get;
            set;
        }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Partner> Partners { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
