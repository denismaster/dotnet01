using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    /// <summary>
    /// Класс, который представляет собой аккаунт пользователя
    /// </summary>
    public class Account:DomainObject
    {

        /// <summary>
        /// Id пользователя
        /// </summary>
        public int Id
        {
            get;
            set;
        }
        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login
        {
            get;
            set;
        }
        /// <summary>
        /// Пароль в виде хеша
        /// </summary>
        public string Password
        {
            get;
            set;
        }
        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Mail
        {
            get;
            set;
        }
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string Role
        {
            get;
            set;
        }
    }
}
