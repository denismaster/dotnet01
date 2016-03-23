using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Models
{
    public class Appointment : DomainObject
    {
        /// <summary>
        /// Ключевое поле
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public Product Product { get; set; }
        public Appointment()
        {
            Schedules = new List<Schedule>();
            OrderItems = new List<OrderItem>();
        }
        public virtual ICollection<Schedule> Schedules { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }

    }
}
