﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace dotnet01.Areas.Manager.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }
        
        public string СourseId { get; set; }

        public string Role { get; set; }
    }
}