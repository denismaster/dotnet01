using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dotnet01.Models;
using System.IO;
namespace dotnet01.Areas.Admin.Models
{
    public  class Test
    {
        public void start()
        {

            using (AccountContext db = new AccountContext())
            {
                var accounts = db.Account.ToList();
           
                StreamWriter file = new StreamWriter("B:\\CourseRepository\\TestFile.txt");
                foreach (var acc in accounts)
                {
                    file.Write(acc.Id + " " + acc.Login + " " + acc.Mail + " " + acc.Role);
                    file.WriteLine();
                }
                file.Close();
                Account newAcc = new Account();
                newAcc.Id  = 10500;
                newAcc.Login = "cool";
                newAcc.Mail = "database";
                newAcc.Password = "works";
                newAcc.Role = "admin";
                db.Account.Add(newAcc);
                db.SaveChanges();
            }

        }
    }
}