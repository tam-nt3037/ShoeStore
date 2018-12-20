using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week02.Models;

namespace Week02.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ShoeStoreEntities db = new ShoeStoreEntities();
        public ActionResult Index()
        {
            List<San_pham> queryProductsAdmin = db.San_pham.Select(m => m).ToList();
            ViewData["queryProductsAdmin"] = queryProductsAdmin;
            return View();
        }
        [HttpGet]
        public ActionResult EditProducts(int id,Nhom_sp nhom_Sp)
        {
            List<San_pham> queryProducts = db.San_pham.Where(n => n.ID_sp.Equals(id)).ToList();
            ViewData["products"] = queryProducts;

            ViewBag.listGroupProducts = new SelectList(db.Nhom_sp, "value", "text", nhom_Sp.ID_nhom).OrderBy(n => n.Text);
            return View();
        }
        [HttpPost]
        public ActionResult EditProducts(San_pham sp, SaveEditModel model)
        {
            int id_sp = model.ID_sp;

            if (ModelState.IsValid)
            {
                var sanpham = db.San_pham.Where(s => s.ID_sp.Equals(id_sp)).First();

                sanpham.Ten_sp = model.Ten_sp;
                sanpham.Hinh_sp = model.Hinh_sp;
                sanpham.ID_nhom = model.ID_nhom;
                sanpham.Mo_ta = model.Mo_ta;
                sanpham.So_luong = model.So_luong;
                sanpham.Gia_sp = model.Gia_sp;
                sanpham.Nsx_sp = model.Nsx_sp;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}