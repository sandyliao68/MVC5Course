using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC5Course
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            /*沒有比對中還給iis執行
             * UrlParameter.Optional-->codeplex.com.
             *  UrlParameter.Optional = null object
             *  *pathInfo-->*等於比對到網址列最後  url: "{controller}/{action}/{*id}", 用/再解析URL
             *  url: "{controller}/{action}.aspx/{id}",-->MVC就可以跑了
             *  url: "{controller}/{action}.php/{id}" <modules runAllManagedModulesForAllRequests="true" /> -->web.config改一下,.php就可以跑了
             *  url: "{controller}.{action}/{id}",  -->Home.Index 小數點( . ) 底線( _ )減號( - ) 都可以             *   
             * 加上路由值的條件約束，使用正則表達式(RegEx) constraints: new { id = @"\d+" }

             */

            //http://localhost/Trace.axd/a/b/c/d/e  -->給IIS執行
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
