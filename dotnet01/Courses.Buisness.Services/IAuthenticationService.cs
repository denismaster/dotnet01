﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courses.Buisness.Services
{
    public interface IAuthenticationService
    {
        bool IsValid(string login, string password);
    }
}
