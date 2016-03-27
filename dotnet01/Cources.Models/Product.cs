using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
   public class Product : DomainObject
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }  //мастер-класс, лекции, курс
        public int PartnerId { get; set; }
        public string Teacher { get; set; }
        public int? SeatsCount { get; set; }
        public int? AssignedUserId { get; set; }
    //    public String imagePath { get; set; }
        public byte[] Image { get; set; }
        public string Location { get; set; }
        //навигационные свойства, не участвуют при инициализции, служат для обращения к связанным сущностям.
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Partner Partner { get; set; }
        public User User { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<Customer> CustomersWithFavouriteProducts { get; set; }
        public virtual ICollection<ProductRating> ProductRatings { get; set; }
        public Product()
        {
            CustomersWithFavouriteProducts = new List<Customer>();
            ProductRatings = new List<ProductRating>();
            Categories = new List<Category>();
            Appointments = new List<Appointment>();
            Comments = new List<Comment>();
            Image = new List<byte>().ToArray();
        }
       
    }
}
