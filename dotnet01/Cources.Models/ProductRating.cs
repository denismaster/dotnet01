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
       
        
        public int CustomerId { get; set; }
        
        public int ProductId { get; set; }
        public int  Rate { get; set; }

       
        public Customer Customer;
        
        public Product Product;
    }
}
