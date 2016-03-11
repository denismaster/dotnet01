using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.ViewModels
{
    public class LoginVewModel
    {
        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string login { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string password { get; set; }

        public bool remember { get; set; }
    }
}
