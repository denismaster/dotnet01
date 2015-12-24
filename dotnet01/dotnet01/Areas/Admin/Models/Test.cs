﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dotnet01.Models;
using System.IO;
using NUnit.Framework;
namespace dotnet01.Areas.Admin.Models
{
    public  class Test
    {
        //fnc<Account, bool> predicate;
        IEnumerable<Account> accounts;
        IAccountRepository repository;

      
        public Test()
        {
            repository = new AccountRepository();
        }
        bool func(Account acc)
        {
            if (acc.Id == 2) return true;
            else return false;
        }
        private void WriteInLogFile(IEnumerable<Account> acccounts)
        {
            StreamWriter file = new StreamWriter("B:\\CourseRepository\\TestFile.txt");
            foreach (var acc in acccounts)
            {
                file.Write(acc.Id + " " + acc.Login + " " + acc.Mail + " " + acc.Role);
                file.WriteLine();
            }
            file.Close();
        }
        public void start()
        {
            accounts =  repository.Get(func, 1, 3);
            WriteInLogFile(accounts);
        }
   
        [Test]
        public void DeleteTest()
        {
            Account ToDelete = repository.Get(2);
            repository.Delete(ToDelete);
            var result = repository.Get(1);
            Assert.Null(result);
        }
    }
}