using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    public class Rating: DomainObject
    {
        /// <summary>
        /// id курса
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// количество проголосовавших положительно
        /// </summary>
        public int Like { get; set; }
        /// <summary>
        /// количество проголосовавших отрицательно
        /// </summary>
        public int Dislike { get; set; }
        /// <summary>
        /// общая оценка
        /// </summary>
        public int Grade { get; set; }
    }
}
