using System;
using MVC5Course.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity.Validation;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();
        // GET: EF
        public ActionResult Index()
        {
           
            db.Product.Add (new Product()
            {               
                
                ProductName ="BMW",
                Price=3,
                Stock=1,
                Active=true

            });

            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                {
                    string entityName = item.Entry.Entity.GetType().Name;
                    foreach (DbValidationError err in item.ValidationErrors)
                    {
                        throw new Exception(entityName + err.ErrorMessage);
                    }
                }
                throw;
            }

            #region Exception另一種寫法
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbEntityValidationException ex)
            //{
            //    //throw ex;
            //    var allErrors = new List<string>();

            //    foreach (DbEntityValidationResult re in ex.EntityValidationErrors)
            //    {
            //        foreach (DbValidationError err in re.ValidationErrors)
            //        {
            //            allErrors.Add(err.ErrorMessage);
            //        }
            //    }

            //    ViewBag.Errors = allErrors;
            //}
            #endregion 

            //取到新增的那一筆ID
            //var data = db.Product.Where(p => p.ProductId == pkey).ToList();
            //排序DESC大到小
            var data = db.Product.OrderByDescending(p => p.ProductId);
            //抓回所有資料清單
            //var data=db.Product.ToList();
            

            return View(data);
        }

       
        public ActionResult Detail(int id)
        {
           // var data = db.Product.Find(id);
            //var data = db.Product.Where(p => p.ProductId == id).FirstOrDefault();
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);
            return View(data);
        }
    }
}