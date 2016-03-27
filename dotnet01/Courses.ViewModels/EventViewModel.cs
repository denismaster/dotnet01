using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class EventViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название сущности")]
        public string Entity { get; set; }

        [DisplayName("Описание события")]
        public string Changes { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Id пользователя *")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Дата создания *")]
        public DateTime CreatedDate { get; set; }
    }
}
