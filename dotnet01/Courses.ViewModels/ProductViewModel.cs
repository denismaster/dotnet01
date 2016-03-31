using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Courses.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            var type = new List<System.Web.Mvc.SelectListItem>();
            type.Add(new System.Web.Mvc.SelectListItem()
            {
                Text = "Курс",
                Value = "1"
            });
            type.Add(new System.Web.Mvc.SelectListItem()
            {
                Text = "Серия лекций",
                Value = "2"
            });
            type.Add(new System.Web.Mvc.SelectListItem()
            {
                Text = "Мастер-класс",
                Value = "3"
            });
            type.Add(new System.Web.Mvc.SelectListItem()
            {
                Text = "Подготовка к экзаменам",
                Value = "4"
            });
            type.Add(new System.Web.Mvc.SelectListItem()
            {
                Text = "Практические занятия",
                Value = "5"
            });
            ProductTypes = type;

        }
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

        public IEnumerable<System.Web.Mvc.SelectListItem> ProductTypes { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Тип курса *")]
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

        [DisplayName("Изображение")]
        public byte[] Image { get; set; }

        [DisplayName("Тип курса *")]
        public String TypeName
        {
            get { return ProductTypes.Where(m => m.Value == Type.ToString()).First().Text; }
            set { }
        }

        [DisplayName("Партнер *")]
        public String PartnerName { get; set; }

        [DisplayName("Ответственный менеджер")]
        public String ManagerName { get; set; }
    }
}
