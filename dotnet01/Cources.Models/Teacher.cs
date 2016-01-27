using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Teacher: DomainObject
    {
        /// <summary>
        /// логин преподавателя 
        /// </summary>
        public string Login { get; set; }
        /// <summary>
        /// пароль преподавателя
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// фИО преподавателя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Email преподавателя
        /// </summary>
        public string Mail { get; set; }
        /// <summary>
        /// Id курса, на котором преподаватель проводит занятия
        /// </summary>
        public string СourseId { get; set; }
        /// <summary>
        /// Роль преподавателя
        /// </summary>
        public string Role { get; set; }
    }
}
