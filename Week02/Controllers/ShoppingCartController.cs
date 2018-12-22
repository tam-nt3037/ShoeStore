using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week02.Models;
namespace Week02.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoeStoreEntities1 db = new ShoeStoreEntities1();

        public ActionResult Index()
        {
            var cart = Session["cart"];
            var email = Session["email"];
            var list = new List<Item>();
            if (cart != null)
            {
                list = (List<Item>)cart;
            }
            // <Output error> when user click 'addtocart' without login
            else if (email == null)
                ViewBag.Error = "Vui lòng đăng nhập để có thể thêm hàng vào giỏ hàng !";
            return View(list);
        }

        private int isExtistingProduct(int id)
        {
            List<Item> cart = (List<Item>)(Session["cart"]);
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].San_pham.ID_sp == id)
                    return i;
            return -1;
        }
        public ActionResult Delete(int id)
        {
            int index = isExtistingProduct(id);
            List<Item> cart = (List<Item>)(Session["cart"]);
            cart.RemoveAt(index);
            Session["cart"] = cart;

            

            return View("Cart");
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            List<Item> cart = (List<Item>)(Session["cart"]);
            ViewData["CartCount"] = cart.Count();
            return PartialView("CartSummary");
        }
        
        public ActionResult OrderNow(int id)
        {
            int quantityProducts = Convert.ToInt32(Request["quantityProducts"]);
            ViewBag.quantityProducts = quantityProducts;

            // Check not click add to cart , click another
            if (id == -2 && Session["cart"] == null)
            {
                // ToDo
            }
            else if (id == -2)
            {
                List<Item> cart = (List<Item>)(Session["cart"]);
                Session["cart"] = cart;
            }
            else if (Session["cart"] == null && Session["email"] == null)
            {
                ModelState.AddModelError("", "Vui lòng đăng nhập để có thể thêm hàng vào giỏ hàng !");
                return RedirectToAction("Index","ShoppingCart");
            }
            else if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.San_pham.Find(id), 1));
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)(Session["cart"]);
                int index = isExtistingProduct(id);
                if (index == -1)
                    cart.Add(new Item(db.San_pham.Find(id), 1));
                else
                {
                    cart[index].So_luong++;
                }
                Session["cart"] = cart;
            }

            //Test
            //var cart = Session["cart"];
            //if (cart != null)
            //{
            //    var list = (List<Item>)cart;
            //    if (list.Exists(x => x.San_pham.ID_sp == id))
            //    {
            //        foreach (var item in list)
            //        {
            //            if (item.San_pham.ID_sp == id)
            //            {
            //                item.So_luong += quantity;
            //            }
            //        }
            //    }
            //    else
            //    {
            //        var item = new Item();
            //        item.San_pham.ID_sp = id;
            //        item.So_luong = quantity;
            //        list.Add(item);
            //    }

            //}
            //else
            //{
            //    var item = new Item();
            //    item.San_pham.ID_sp = id;
            //    item.So_luong = quantity;
            //    var list = new List<Item>();
            //    list.Add(item);
            //    Session["cart"] = list;
            //}
            // test

            return View("Cart");
        }
        
        [HttpPost]
        public ActionResult OrderMoreItem(int id)
        {
            int quantityProducts = Convert.ToInt32(Request["quantityProducts"]);
            ViewBag.quantityProducts = quantityProducts;

            // Check not click add to cart , click another
            if (id == -2 && Session["cart"] == null)
            {
                // ToDo
            }
            else if (id == -2)
            {
                List<Item> cart = (List<Item>)(Session["cart"]);
                Session["cart"] = cart;
            }
            else if (Session["cart"] == null && Session["email"] == null)
            {
                ModelState.AddModelError("", "Vui lòng đăng nhập để có thể thêm hàng vào giỏ hàng !");
                return RedirectToAction("Index", "ShoppingCart");
            }
            else if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(db.San_pham.Find(id), quantityProducts));
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)(Session["cart"]);
                int index = isExtistingProduct(id);
                if (index == -1)
                    cart.Add(new Item(db.San_pham.Find(id), quantityProducts));
                else
                {
                    cart[index].So_luong++;
                }
                Session["cart"] = cart;
            }
            return View("Cart");
        }
        
        public ActionResult Update(FormCollection fc)
        {
            string[] quantities = fc.GetValues("quantityProducts");
            List<Item> cart = (List<Item>)(Session["cart"]);
            for (int i = 0; i < cart.Count; i++)
                cart[i].So_luong = Convert.ToInt32(quantities[i]);
            Session["cart"] = cart;
            //int quantityProducts = Convert.ToInt32(Request["quantityProducts"]);
            //ViewBag.quantityProducts = quantityProducts;


            return View("Cart");
        }


    }
}