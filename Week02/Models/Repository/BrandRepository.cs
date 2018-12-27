using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week02.Models.Repository
{
    public class BrandRepository
    {
        ShoeStoreEntities1 ctx;

        public BrandRepository()
        {
            ctx = new ShoeStoreEntities1();
        }
        public List<Nhan_hieu> getAllBrands()
        {
            return ctx.Nhan_hieu.ToList();
        }
    }
}