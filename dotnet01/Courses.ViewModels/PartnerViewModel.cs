using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class PartnerViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public DateTime UpdatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int? UserId { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Contact { get; set; }


    }
}
