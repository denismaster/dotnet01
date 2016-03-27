using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        [DisplayName("Текст комментария")]
        public string Text { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Дата создания *")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Дата обновления *")]
        public DateTime UpdatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Id пользователя *")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Id продукта *")]
        public int ProductId { get; set; }
    }
}
