﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Customer : DomainObject
    {
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
        public string PasswordResetToken { get; set; }
        public string Email { get; set; }
        public byte Role { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Gender { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EmailQueue> EmailQueues { get; set; }
     
        public virtual ICollection<Product> FavouriteProducts { get; set; }
        public virtual ICollection<ProductRating> ProductRatings { get; set; }
        public Customer()
        {
            Orders = new List<Order>();
            Comments = new List<Comment>();
            EmailQueues = new List<EmailQueue>();
            FavouriteProducts = new List<Product>();
            ProductRatings = new List<ProductRating>();
                 
        }
      
        
    }
}
