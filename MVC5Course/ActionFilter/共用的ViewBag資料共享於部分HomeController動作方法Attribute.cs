using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class 共用的ViewBag資料共享於部分HomeController動作方法Attribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewBag.Message = "改用共用的ViewBag資料共享於部分HomeController動作方法Attribute";
            base.OnActionExecuting(filterContext);
        }
    }
}
