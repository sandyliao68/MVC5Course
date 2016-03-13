using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //改用 repo
        //private FabricsEntities db = new FabricsEntities(); 
        ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index()
        {
            var data = repo.All().Take(5);

            return View(data);
        }

        [HttpPost]
        public ActionResult Index(IList<Produsts批次更新ViewModel> data)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in data)
                {
                    var product = repo.Find(item.ProductId);
                    product.Stock = item.Stock;
                    product.Price = item.Price;
                }
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(repo.All().Take(5));    //重新回到輸入錯誤的狀態
        }



       

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            //Product product = repo.All().FirstOrDefault(p => p.ProductId == id);
            Product product= repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }

            

            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Product.Add(product);
                //db.SaveChanges();
                repo.Add(product);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        // 使用 TryUpdateModel 做延遲驗證
        //public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)    //會BIND到預設值
        public ActionResult Edit(int id, FormCollection form)   // FormCollection form-->沒用到只是為了不重複Edit
        {
            /*
             * 改成介面有輸入驗證屬性
             * 模型繫結延遲驗證  
            * 透過介面限制傳入參數的數量(白名單機制)
            * TryUpdateModel<IUserInputModel>(user);
            * 範例：http://bit.ly/YvHnnx
             */
            Product product = repo.Find(id);    //從DB取得完整資料
            if (TryUpdateModel<Product>(product, new string[] {
                 "ProductId","ProductName","Price","Active","Stock" })) //BIND部份屬性(有提供修改的欄位)
            {
                repo.UnitOfWork.Commit();
            }
            TempData["ProductsEditDoneMsg"] = "商品編輯成功!";    //用完一次就清空
            return RedirectToAction("Index");
            //原本的程式碼
            //if (ModelState.IsValid)
            //{
            //    var db = (FabricsEntities)repo.UnitOfWork.Context;
            //    db.Entry(product).State = EntityState.Modified;
            //    db.SaveChanges();
            //    TempData["ProductsEditDoneMsg"] = "商品編輯成功!";
            //    return RedirectToAction("Index");
            //}
            //return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Product product = db.Product.Find(id);
            Product product = repo.Find(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Product product = db.Product.Find(id);
            Product product = repo.Find(id);
            product.IsDeleted = true;
            //db.Product.Remove(product);
           // db.SaveChanges();
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                var db = (FabricsEntities)repo.UnitOfWork.Context;
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
