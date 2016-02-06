using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Courses.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Название мероприятия *")]
        public string Name { get; set; }

        [DisplayName("Описание")]
        public string Description { get; set; }

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

        [Required]
        [DisplayName("Активность мероприятия")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Тип мероприятия *")]
        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int Type { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Партнер *")]
        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int PartnerId { get; set; }

        [DisplayName("Преподаватель")]
        public string Teacher { get; set; }

        [DisplayName("Количество мест")]
        //[RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int? SeatsCount { get; set; }

        [DisplayName("Ответственный менеджер")]
        //[RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        public int? AssignedUserId { get; set; }

        [DisplayName("Место проведения")]
        public string Location { get; set; }
    }
}
