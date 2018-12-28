using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Week02.Models;
using Week02.Models.Repository;

namespace Week02.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        ShoeStoreEntities1 db = new ShoeStoreEntities1();

        private ProductGroupRepository _pRep;
        private BrandRepository _bRep;
        private ProductRepository _proRep;

        public AdminController()
        {
            _pRep = new ProductGroupRepository();
            _bRep = new BrandRepository();
            _proRep = new ProductRepository();
        }

        public ActionResult Index()
        {
            List<San_pham> queryProductsAdmin = db.San_pham.Select(m => m).ToList();
            ViewData["queryProductsAdmin"] = queryProductsAdmin;
            return View();
        }

        public ActionResult DeleteProduct(int id_sp)
        {
            _proRep.DeleteProduct(id_sp);

            return RedirectToAction("index");
        }

        public ActionResult CreateProduct()
        {
            List<Nhom_sp> product_group = _pRep.getAllProductGroup();
            List<Nhan_hieu> product_brand = _bRep.getAllBrands();

            ProductModel model = new ProductModel()
            {
                ProductGroup = product_group,
                Brands = product_brand

            };

            return View(model);
        }




        [HttpPost]
        public ActionResult CreateProduct(HttpPostedFileBase upload, ProductModel model)
        {
            List<Nhom_sp> product_group = _pRep.getAllProductGroup();
            List<Nhan_hieu> product_brand = _bRep.getAllBrands();

            //Save info
            if (ModelState.IsValid)
            {
                //Save file

                if (upload != null && upload.ContentLength > 0)
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/images/product"),
                                                   Path.GetFileName(upload.FileName));
                        upload.SaveAs(path);
                        //ViewBag.Message = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }

                San_pham sp = new San_pham()
                {
                    ID_sp = 0,
                    Ten_sp = model.Ten_sp,
                    Nsx_sp = model.Nsx_sp,
                    Gia_sp = model.Gia_sp,
                    Hinh_sp = upload.FileName,
                    Mo_ta = model.Mo_ta,
                    ID_nhom = model.ID_nhom,
                    So_luong = model.So_luong

                };
                _proRep.SaveProduct(sp);
                ViewBag.Message = "Create product successfully";
            }

            


            model = new ProductModel()
            {
                ProductGroup = product_group,
                Brands = product_brand

            };

            
            return View(model);
        }

        [HttpGet]
        public ActionResult EditProducts(int id, Nhom_sp nhom_Sp)
        {
            List<Nhom_sp> product_group = _pRep.getAllProductGroup();
            List<Nhan_hieu> product_brand = _bRep.getAllBrands();

            San_pham products = db.San_pham.Where(n => n.ID_sp.Equals(id)).FirstOrDefault();

            ProductModel model = new ProductModel()
            {
                ProductGroup = product_group,
                Brands = product_brand,

                ID_sp = products.ID_sp,
                Ten_sp = products.Ten_sp,
                Gia_sp = (int)products.Gia_sp,
                Hinh_sp = products.Hinh_sp,
                Mo_ta = products.Mo_ta,
                ID_nhom = products.ID_nhom,
                Nsx_sp = products.Nsx_sp,
                So_luong = (int)products.So_luong
                
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult EditProducts(San_pham sp, ProductModel model, HttpPostedFileBase uploadEdit)
        {
            int id_sp = model.ID_sp;

            //Save info
            if (ModelState.IsValid)
            {
                //Save file
                var sanpham = db.San_pham.Where(s => s.ID_sp.Equals(id_sp)).First();

                sanpham.Ten_sp = model.Ten_sp;
                
                sanpham.ID_nhom = model.ID_nhom;
                sanpham.Mo_ta = model.Mo_ta;
                sanpham.So_luong = model.So_luong;
                sanpham.Gia_sp = model.Gia_sp;
                sanpham.Nsx_sp = model.Nsx_sp;
                

                if (uploadEdit != null && uploadEdit.ContentLength > 0)
                    try
                    {
                        string path = Path.Combine(Server.MapPath("~/images/product"),
                                                   Path.GetFileName(uploadEdit.FileName));
                        uploadEdit.SaveAs(path);
                        sanpham.Hinh_sp = uploadEdit.FileName;

                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "ERROR:" + ex.Message.ToString();
                    }
                else
                {
                    ViewBag.Message = "You have not specified a file.";
                }


                db.SaveChanges();
                ViewBag.Message = "Updated product successfully";
            }

            //if (ModelState.IsValid)
            //{
            //    var sanpham = db.San_pham.Where(s => s.ID_sp.Equals(id_sp)).First();

            //    sanpham.Ten_sp = model.Ten_sp;
            //    sanpham.Hinh_sp = model.Hinh_sp;
            //    sanpham.ID_nhom = model.ID_nhom;
            //    sanpham.Mo_ta = model.Mo_ta;
            //    sanpham.So_luong = model.So_luong;
            //    sanpham.Gia_sp = model.Gia_sp;
            //    sanpham.Nsx_sp = model.Nsx_sp;
            //    db.SaveChanges();
            //}
            return RedirectToAction("index");
        }
    }
}