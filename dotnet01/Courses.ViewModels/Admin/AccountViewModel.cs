using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Courses.ViewModels
{
    /// <summary>
    /// Класс, который представляет собой ViewModel для аккаунтов.
    /// DTO с валидационной логикой.
    /// </summary>
    public class AccountViewModel
    {
        public AccountViewModel()
        {
            var roles = new List<System.Web.Mvc.SelectListItem>();
            roles.Add(new System.Web.Mvc.SelectListItem()
                {
                    Text = "Default",
                    Value = "Default"
                });
            roles.Add(new System.Web.Mvc.SelectListItem()
            {
                Text = "Manager",
                Value = "Manager"
            });
            roles.Add(new System.Web.Mvc.SelectListItem()
            {
                Text = "Admin",
                Value = "Admin"
            });
            Roles = roles;
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Login { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [StringLength(15, MinimumLength = 5, ErrorMessage = "*длина должна быть от 5 до 15 символов")]
        public string Password { get; set; }

        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "*неверный адрес")]
        public string Email { get; set; }

        [Required(ErrorMessage = "*поле должно быть заполнено")]
        public bool IsActive
        {
            get;
            set;
        }

        public IEnumerable<System.Web.Mvc.SelectListItem> Roles
        {
            get;
            set;
        }
        public string Role { get; set; }


    }
}
