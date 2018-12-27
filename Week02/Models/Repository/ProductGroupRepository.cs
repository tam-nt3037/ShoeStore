using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Week02.Models.Repository
{
    public class ProductGroupRepository
    {
        ShoeStoreEntities1 ctx;

        public ProductGroupRepository()
        {
            ctx = new ShoeStoreEntities1();
        }
        public List<Nhom_sp> getAllProductGroup()
        {
            return ctx.Nhom_sp.ToList();
        }
    }
}