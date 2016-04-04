using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class CategoryViewModel : IEquatable<CategoryViewModel>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Название категории *")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Дата создания *")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Дата обновления *")]
        public DateTime UpdatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [DisplayName("Активность категории *")]
        public bool Active { get; set; }

        [DisplayName("Родительская категория")]
        public int? ParentCategoryId { get; set; }

        [DisplayName("Описание")]
        public String Description { get; set; }

        public bool Equals(CategoryViewModel other)
        {
            if (other == null) return false;
            return (this.Id.Equals(other.Id));
        }
    }
}

