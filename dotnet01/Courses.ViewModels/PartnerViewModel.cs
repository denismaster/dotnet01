using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("ФИО партнера *")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Дата создания *")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        //[DateRange("01/12/2000", "01/12/2100")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Дата обновления *")]
        [DataType(DataType.Date)]
        //[DateRange("01/12/2000", "01/12/2100")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime UpdatedDate { get; set; }

        [DisplayName("User id")]
        //[RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int? UserId { get; set; }

        [DisplayName("Адресс")]
        public string Address { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Контакты")]
        public string Contact { get; set; }
    }
}
