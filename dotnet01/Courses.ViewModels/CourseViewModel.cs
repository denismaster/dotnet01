using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class CourseViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Title { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [RegularExpression(@"\d{1,}\.?\d{1,}", ErrorMessage = "введите число")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int Length { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Dates { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Location { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Teacher { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Organizer { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Description { get; set; }

        public byte[] Image { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Active { get; set; }
    }
}
