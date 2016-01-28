using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public DateTime UpdatedDate { get; set; }

        public bool Active { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int Type { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int PartnerID { get; set; }

        public string Teacher { get; set; }

        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int SeatsCount { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int AssignedUserID { get; set; }

        public string Location { get; set; }
    }
}
