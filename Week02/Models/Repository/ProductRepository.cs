using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week02.Models.Repository
{
    public class ProductRepository
    {
        ShoeStoreEntities1 ctx;

        public ProductRepository()
        {
            ctx = new ShoeStoreEntities1();
        }
        public void SaveProduct(San_pham sp)
        {
            ctx.San_pham.Add(sp);
            ctx.SaveChanges();
        }
    }
}