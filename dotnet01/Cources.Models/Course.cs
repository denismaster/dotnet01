using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Models
{
    /// <summary>
    /// Класс, который представляет собой курс
    /// </summary>
    public class Course: DomainObject
    {
        /// <summary>
        /// заголовок курса
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// стоимость курса
        /// </summary>
        public double Cost { get; set; }
        /// <summary>
        /// длительность проведения курсов
        /// </summary>
        public int Length { get; set; }
        /// <summary>
        /// дата проведения
        /// </summary>
        public string Dates { get; set; }
        /// <summary>
        /// месторасположение проведения занятий
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        /// преподователь курсов
        /// </summary>
        public string Teacher { get; set; }
        /// <summary>
        /// организатор курсов (комания или образовательный центр)
        /// </summary>
        public string Organizer { get; set; }
        /// <summary>
        /// краткое описание курса
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// изображение курса
        /// </summary>
        public byte[] Image { get; set; }
        /// <summary>
        /// актуальность(активность) курса
        /// </summary>
        public string Active { get; set; }
    }
}
