using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace dotnet01.Areas.Manager.Models
{
    public class Course
    {

        public int Id { get; set; }

        //[Required(ErrorMessage = "*поле должно быть заполнено")]
        public string Title { get; set; }

        /*
        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [RegularExpression(@"\d{1,}\.?\d{1,}", ErrorMessage = "введите число")]
        */
        public double Cost { get; set; }

        /*
        [Required(ErrorMessage = "*поле должно быть заполнено")]
        [RegularExpression(@"\d{1,}", ErrorMessage = "введите число")]
        */
        public int Length { get; set; }

        public string Dates { get; set; }

        public string Location { get; set; }

        public string Teacher { get; set; }

        public string Organizer { get; set; }

        public string Description { get; set; }

        public byte[] Image { get; set; }

        //----курс активный/неактивный (отображается/не отображается обычнму юзеру)
        public string Active { get; set; }
    }

}