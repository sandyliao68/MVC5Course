using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => !p.IsDeleted);
          
        }

        public IQueryable<Product> All(bool isAll)
        {
            if (isAll)
            {
                //нье═
                return base.All();
            }
            else
            {
                //Repository
                return this.All();
            }
        }

         public Product Find(int id)
         {
             return this.All().FirstOrDefault(p => p.ProductId == id);
         }

	}

	public  interface IProductRepository : IRepository<Product>
	{

	}
}