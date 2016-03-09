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
        public int Id { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public DateTime UpdatedDate { get; set; }

        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int? UserId { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Contact { get; set; }


    }
}
