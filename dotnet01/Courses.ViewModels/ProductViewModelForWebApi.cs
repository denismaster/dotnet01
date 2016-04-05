using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class ProductViewModelForWebApi
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
        public int? SeatsCount { get; set; }

        [DisplayName("Ответственный менеджер")]
        public int? AssignedUserId { get; set; }

        [DisplayName("Место проведения")]
        public string Location { get; set; }

        [DisplayName("Изображение")]
        public byte[] Image { get; set; }

        [DisplayName("Тип курса *")]
        public String TypeName { get; set; }

        [DisplayName("Партнер *")]
        public String PartnerName { get; set; }

        [DisplayName("Ответственный менеджер")]
        public String ManagerName { get; set; }

        /// <summary>
        /// Список названий категорий, к которым принадлежит данный курс
        /// </summary>
        public List<String> CategoriesNames { get; set; }

        /// <summary>
        /// Список всех категорий в виде одной строки ( c# ; java ; 
        /// </summary>
        public String CategoriesNamesString { get; set; }

    }
}
