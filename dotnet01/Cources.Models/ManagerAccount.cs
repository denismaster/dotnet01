using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class ManagerAccount: DomainObject
    {
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// Пароль в виде хеша
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// ФИО менеджера
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Номер телефона менеджера
        /// </summary>
        public string TelephoneNumber { get; set; }
        /// <summary>
        /// Email менеджера
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Роль менеджера
        /// </summary>
        public string Role { get; set; }
    }
}
