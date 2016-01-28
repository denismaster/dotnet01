using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Courses.Models
{
    public class ProductRating : DomainObject
    {
       
        [Key,Column(Order = 0)]
        public int CustomerId { get; set; }
        [Key,Column(Order = 1)]
        public int ProductId { get; set; }
        public int  Rate { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer;
        [ForeignKey("ProductId")]
        public Product Product;
    }
}
