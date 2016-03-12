using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class TestController : BaseController
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MemberProfile()
        {
            return View();
        }

        //MVCP TAB TAB
        [HttpPost]
        public ActionResult MemberProfile(MemberViewModel data)
        {
            return View();
        }
    }
}