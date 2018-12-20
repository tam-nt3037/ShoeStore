using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
// Khai báo thư viện Models
using Week02.Models;
using Week02.common;
namespace Week02.Controllers
{
    public class HomeController : Controller
    {
        ShoeStoreEntities db = new ShoeStoreEntities();
        public ActionResult Index()
        {

            List<San_pham> queryProducts = db.San_pham.OrderByDescending(n => n.ID_sp).Select(m => m).Take(6).ToList();
            ViewData["products"] = queryProducts;

            //var queryBill = (from h in db.CT_HoaDon
            //                 group h by new { h.ID_sp } into hh
            //                 select new
            //                 {
            //                     hh.Key.ID_sp,
            //                     So_luong = hh.Sum(s => s.So_luong)
            //                 }).OrderByDescending(i => i.ID_sp).ToList();

            //List<CT_HoaDon> queryBill = db.CT_HoaDon.OrderBy(n => n.ID_sp).Select(m => m).Take(6).ToList();
            
            return View();


        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            
            return View();
        }
        public ActionResult Faqs()
        {
            ViewBag.Message = "Your faqs page.";
            
            return View();
        }
        public ActionResult Products(int id)
        {
            if (id.Equals(0))
            {
                List<San_pham> queryProducts = db.San_pham.Select(n => n).ToList();
                
                return View(queryProducts);
            }
            else {
                List<San_pham> qr = db.San_pham.Where(s => s.ID_nhom == (Int16)id).ToList();
                List<Nhom_sp> getNhom_sp = db.Nhom_sp.Where(s => s.ID_nhom == (Int16)id).ToList();
                ViewData["getNhom_sp"] = getNhom_sp;
                return View(qr);
               
            }

        }
        public ActionResult Checkout(CheckoutViewModel model)
        {
            List<Item> cart = (List<Item>)(Session["cart"]);
            Session["cart"] = cart;
            string email = (String)Session["email"];
            if (cart == null)
                ViewBag.ErrorNotAddToCart = "Vui lòng chọn sản phẩm mà bạn thích vào trong giỏ hàng !!!";
            else if (cart != null && email != null)
            {
                List<Khach_hang> details_cus = (List<Khach_hang>)db.Khach_hang.Where(n => n.Email_kh == email).Select(n => n).ToList();
                ViewData["CustomerInfor"] = details_cus;
            }
            return View();
        }
        [HttpPost]
        public ActionResult Checkout(Khach_hang kh,Hoa_don hd,CT_HoaDon ctHD, CheckoutViewModel model)
        {
            List<Item> cart = (List<Item>)(Session["cart"]);
            Session["cart"] = cart;
            string email = (String)Session["email"];
            if (ModelState.IsValid)
            {
                if (cart == null || email == null)
                    ViewBag.ErrorNotAddToCart = "Vui lòng chọn sản phẩm mà bạn thích vào trong giỏ hàng !!!";
                else if (cart != null && email != null )
                {
                    findIdByEmail(email);

                    // Compute sum of cart
                    Session["sumMoney"] = 0;
                    foreach (var p in cart)
                    {
                        int item = Convert.ToInt32(p.San_pham.Gia_sp * p.So_luong);
                        Session["sumMoney"] = Convert.ToInt32(Session["sumMoney"]) + item;
                    }
                    // End Compute sum of cart
                    
                    placeOrder(cart);

                    Session["cart"] = null;
                    ViewBag.SuccessCheckout = "Đơn hàng đã đặt thành công !!!";

                }
                else
                {
                    ViewBag.ErrorNotAddToCart = "Vui lòng chọn sản phẩm mà bạn thích vào trong giỏ hàng !!!";
                }
            }
            return View();
        }
        
        public void placeOrder(List<Item> cart) {
            if (checkQuantityOnHand(cart))
            {
                // Create Hoa don 1,2,3,
                if (CREATE_ORDERS())
                {
                    // Create CT Hoa don
                    CREATE_ORDERS_DETAILS(cart);

                    ViewBag.stateCheckout = "success";

                }


            }
            else
            {
                ViewBag.checkQuantityProduct = "false";
                ViewBag.messageCheckQuantityProduct = "Sản phẩm trong kho không đủ với với số lượng bạn cần";
            }

           
        }

        public bool checkQuantityOnHand(List<Item> cart) {

            foreach (Item item in cart)
            {
                // Get ID_SP to condition quantity
                var result = db.San_pham.SingleOrDefault(b => b.ID_sp.Equals(item.San_pham.ID_sp));
                if ((result.So_luong - item.So_luong) >= 0)
                {
                    return true;
                }
            }
            return false;  

        }
       
        public bool CREATE_ORDERS()
        {

            try
            {
                Hoa_don hd = new Hoa_don();
                hd.ID_kh = Convert.ToInt32(Session["idCus"]);
                hd.Ngay_lap = DateTime.Now;
                hd.Tong_tien = Convert.ToInt32(Session["sumMoney"]);
                hd.Trang_thai = "CTT";
                hd.Tien_ship = 5;
                hd.Trangthai_xulyhd = "Dang Tien Hanh";
                db.Hoa_don.Add(hd);
                db.SaveChanges();

                return true;
            }
            catch (Exception ex) {
                return false;
            }

        }
        public void CREATE_ORDERS_DETAILS(List<Item> cart) {
                     
            List<Hoa_don> lst_mahd = db.Hoa_don.OrderByDescending(m => m.Ma_hd).Select(n => n).Take(1).ToList();
                      
            
            foreach (Item item in cart)
            {
                CT_HoaDon ctHD = new CT_HoaDon();
                ctHD.Ma_hd = lst_mahd[0].Ma_hd;
                ctHD.ID_sp = item.San_pham.ID_sp;
                ctHD.So_luong = item.So_luong;
                ctHD.Gia = item.San_pham.Gia_sp;
                db.CT_HoaDon.Add(ctHD);


                // Get ID_SP to condition quantity
                var result = db.San_pham.SingleOrDefault(b => b.ID_sp.Equals(item.San_pham.ID_sp));
                // Update Quantity Products
                if ((result.So_luong - item.So_luong) >= 0)
                    result.So_luong = result.So_luong - item.So_luong;


                db.SaveChanges();
            }
        }

        public int findIdByEmail(string email)
        {
            List<Khach_hang> id = db.Khach_hang.Where(m => m.Email_kh.Equals(email)).Select(n => n).ToList();
            Session["idCus"] = id[0].ID_kh;
            return 0;
        }
        public ActionResult ProductDetail(int id, string Nsx_sp)
        {
            List<San_pham> qr = db.San_pham.Include("Nhom_sp").Where(s => s.ID_sp == (Int16)id && s.Nsx_sp.Equals(Nsx_sp)).ToList();
            List<Item> cart = (List<Item>)(Session["cart"]);
            Session["cart"] = cart;
            return View(qr);
        }

        [ChildActionOnly]
        public ActionResult loadCategory()
        {
            //var categories = (from d in db.San_pham
            //                  group d by d.Nsx_sp into g
            //                  select new { nsx_sp = g.Key });
            //return PartialView(categories.ToList());
            var getCategory = db.San_pham.GroupBy( m => m.Nsx_sp)
                                    .Select( n => n.FirstOrDefault());
            return PartialView(getCategory.ToList());
        }
        [ChildActionOnly]
        public ActionResult loadCategoryProduct()
        {
            List<Nhom_sp> getCategoryProduct = db.Nhom_sp.Select(n => n).ToList();
            ViewData["getCategoryProduct"] = getCategoryProduct;
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult loadBestsellers()
        {
            List<BestSellersModel> best = (List<BestSellersModel>)db.CT_HoaDon
                                .GroupBy(k => new { k.ID_sp })
                                .Select(c => new BestSellersModel
                                {
                                    ID_sp = c.Select(q => q.ID_sp).FirstOrDefault(),
                                    So_luong = c.Sum(q => q.So_luong).Value
                                }).OrderByDescending(f => f.So_luong).Take(5).ToList();
            ViewData["bestsellers"] = best;

            List<San_pham> getAllProducts = db.San_pham.Select(n => n).ToList();
            ViewData["getAllProducts"] = getAllProducts;
            return PartialView();
        }
        public ActionResult findByCategory(string Nsx_sp)
        {
            List<San_pham> cate = db.San_pham.Where(s => s.Nsx_sp.Equals(Nsx_sp)).ToList();
            ViewBag.Nsx_sp = Nsx_sp;
            return View(cate);
        }
        public ActionResult loadRelatedProducts(string Nsx_sp)
        {
            // RANDOM ROWS IN ENTITY FRAMWORK USE => ORDERBY ( R => GUID.NEWGUID())
            List<San_pham> getRelatedProducts = db.San_pham.Where(n => n.Nsx_sp.Equals(Nsx_sp)).OrderBy(r => Guid.NewGuid()).Take(3).ToList();
            ViewData["getRelatedProducts"] = getRelatedProducts;
            return PartialView();
        }
        [HttpPost]
        public ActionResult Search(string keyword) {

            keyword = Request["keyword"];
            ViewBag.keyword = keyword;
            List<San_pham> lst_search = db.San_pham.Where(n => n.Ten_sp.Contains(keyword)).ToList();
            ViewData["lst_search"] = lst_search;
            return View();
        }
    }
}