using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    public class RegisterVeiwModel
    {
        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string login { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "*неверный адрес")]
        public string email { get; set; }

        [StringLength(15, MinimumLength = 5, ErrorMessage = "*длина должна быть от 5 до 15 символов")]
        public string password { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string repeat { get; set; }
    }
}
