using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public DateTime UpdatedDate { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public bool Active { get; set; }

        public int? ParentCategoryId { get; set; }

        public String Description { get; set; }
    }
}

