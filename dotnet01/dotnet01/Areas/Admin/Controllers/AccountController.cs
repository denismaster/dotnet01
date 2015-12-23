using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace dotnet01.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        //TODO:define IAccountRepository here
        public AccountController()
        {
            //TODO:init repository with our implementation
        }
        public ActionResult New()
        {
            //Create AccountNewViewModel and send it to View via parameter
            return View();
        }
        public ActionResult Edit(int id)
        {
            //Create AccountEditViewModel and send it to View via parameter
            //Actions are similar, can use only one model
            return View();
        }
        public ActionResult Index()
        {
            //Create AccountIndexViewModel and send it to View via parameter
            return View();
        }
    }
}