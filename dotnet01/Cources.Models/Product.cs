using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Product: DomainObject
    {
        public int ID { get; set;}
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Active { get; set; }
        public int Type { get; set; }
        public int PartnerID { get; set; }
        public string Teacher { get; set; }
        public int SeatsCount { get; set; }
        public int AssignedUserID { get; set; }
        public string Location { get; set; }

    }
}
