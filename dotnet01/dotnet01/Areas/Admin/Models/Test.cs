﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using dotnet01.Models;
using System.IO;
using NUnit.Framework;
namespace dotnet01.Areas.Admin.Models
{
    public class Test
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
        [Test]
        public void PaginationTest()
        {
            List<Account> page = repository.Get(1, 3, item => item.Id == 3).ToList();
            page.ForEach(item => Log.Write(item.Login));
            Assert.NotNull(page);
        }
        [Test]
        public void EditTest()
        {
            Account acc = repository.Get(3);

            acc.Login = "testEdit";
            repository.Edit(acc);
            repository.SaveChanges();
           
        }
        [Test]
        public void FilterTest()
        {
            FieldFilter fieldFilter1 = new FieldFilter();
            fieldFilter1.Name = "LogIn";
            fieldFilter1.Value = "te";
            FieldFilter fieldFilter2 = new FieldFilter();
            fieldFilter2.Name = "LogIn";
            fieldFilter2.Value = "cool";
            FieldFilter fieldFilter3 = new FieldFilter();
            fieldFilter2.Name = "LogIn";
            fieldFilter2.Value = "14";
            SortFilter sortFilter = new SortFilter();
            sortFilter.SortOrder = "LogIn";
            List<FieldFilter> list = new List<FieldFilter>();
            list.Add(fieldFilter1);
            list.Add(fieldFilter2);
            list.Add(fieldFilter3);
           IEnumerable<Account> l = repository.Get(1, 3, list,sortFilter);
            foreach(Account acc in l)
            {
                Log.Write(acc.Login);
            }
        }
    }
}