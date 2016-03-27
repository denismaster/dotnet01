using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Id пользователя *")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Дата создания *")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Дата обновления *")]
        public DateTime UpdateDate { get; set; }
    }
}
