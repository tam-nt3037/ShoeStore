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
        public void DeleteProduct(int pid)
        {
            San_pham p_find = ctx.San_pham.Where(p => p.ID_sp.Equals(pid)).SingleOrDefault();
            if (p_find != null)
            {
                ctx.San_pham.Remove(p_find);
            }
            ctx.SaveChanges();
        }
    }
}