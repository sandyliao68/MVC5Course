using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public abstract class BaseController : Controller
    {
        protected override void HandleUnknownAction(string actionName)
        {
            //找不到網頁回到首頁
            this.Redirect("/").ExecuteResult(this.ControllerContext);
            //base.HandleUnknownAction(actionName);
        }

        protected ProductRepository repo = RepositoryHelper.GetProductRepository();
        // GET: Base
        public ActionResult Debug()
        {
            return Content("DEBUG");
        }
    }
}