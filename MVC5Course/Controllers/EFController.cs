using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVC5Course.Controllers
{
    public class EFController : Controller
    {
        FabricsEntities db = new FabricsEntities();

        public ActionResult Index(bool? IsActive, string keyword)
        {
            var product = new Product()
            {
                ProductName = "BMW",
                Price = 2,
                Stock = 1,
                Active = true
            };

            //db.Product.Add(product);
            //SaveChanges();

            var pkey = product.ProductId;

            //var data = db.Product.Where(p => p.ProductId == pkey).ToList();
            //http://localhost:10681/EF/Index?IsActive=true&keyword=BMW
            var data = db.Product.OrderByDescending(p => p.ProductId).AsQueryable();

            if (IsActive.HasValue)
            {
                data = data.Where(p => p.Active.HasValue ? p.Active.Value == IsActive.Value : false);
            }
            if (!String.IsNullOrEmpty(keyword))
            {
                data = data.Where(p => p.ProductName.Contains(keyword));
            }

            foreach (var item in data)
            {
                item.Price = item.Price + 1;
            }

            SaveChanges();

            return View(data);
        }

        private void SaveChanges()
        {
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
                        throw new Exception(entityName + " 類型驗證失敗: " + err.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public ActionResult Detail(int id)
        {
            //var data = db.Product.Find(id);
            //var data = db.Product.Where(p => p.ProductId == id).FirstOrDefault();
            var data = db.Product.FirstOrDefault(p => p.ProductId == id);

            return View(data);
        }

        public ActionResult Delete(int id)
        {
            var product = db.Product.Find(id);

            //foreach (var ol in db.OrderLine.Where(p => p.ProductId == id).ToList())
            //{
            //    db.OrderLine.Remove(ol);
            //}

            //foreach (var ol in product.OrderLine.ToList())
            //{
            //    db.OrderLine.Remove(ol);
            //}

            db.OrderLine.RemoveRange(product.OrderLine);

            db.Product.Remove(product);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult QueryPlan(int num = 10)
        {
            //效能最差
           // var data = db.Product.OrderBy(p => p.ProductId).AsQueryable();
            //正確用法
            var data = db.Product.Include(t => t.OrderLine).OrderBy(p => p.ProductId).AsQueryable();
            //直接下SQL語法
//           var data = db.Database.SqlQuery<Product>(@"
//                 SELECT * 
//                 FROM dbo.Product p 
//                 WHERE p.ProductId < @p0", num);
            return View(data);
        }

    }
}