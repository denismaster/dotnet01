using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dotnet01.Areas.Manager.Models
{
    public class Rating
    {
        public int Id { get; set; }

        public string CourseId { get; set; }

        public int Like { get; set; }

        public int Dislike { get; set; }

        public int Grade { get; set; }

    }
}