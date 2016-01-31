using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
   public class Product : DomainObject
    { 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public int PartnerId { get; set; }
        public string Teacher { get; set; }
        public int SeatsCount { get; set; }
        public int? AssignedUserId { get; set; }
        public string Location { get; set; }

        public List<Appointment> Appointments { get; set; }
        public List<Comment> Comments { get; set; }

        public Partner Partner { get; set; }
        public User User { get; set; }
        public List<Category> Categories { get; set; }
   
       
        public List<Customer> CustomersWithFavouriteProducts { get; set; }
        public List<ProductRating> ProductRatings { get; set; }
       
    }
}
