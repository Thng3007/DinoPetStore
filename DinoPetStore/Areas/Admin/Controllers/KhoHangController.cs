using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinoPetStore.App_Start;
using DinoPetStore.EF;
using PagedList;

namespace DinoPetStore.Areas.Admin.Controllers
{
    [AdminPhanQuyen(MACHUCNANG = "QL_KHOSANPHAM")]
    public class KhoHangController : Controller
    {
        DinoStoreDbContext data = new DinoStoreDbContext();

        // GET: Admin/KhoHang
        public ActionResult Index(int? page, int? pageSize)
        {
            if(Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }

            else
            {
                if (page == null)
                {
                    page = 1;
                }
                if (pageSize == null)
                {
                    pageSize = 10;
                }

                var khohang = data.PHIEUNHAPKHOes.ToList();
                return View(khohang.ToPagedList((int)page, (int)pageSize));

            }
        }

        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                var khohang = from kt in data.PHIEUNHAPKHOes where kt.MAPHIEUNK == id select kt;
                return View(khohang.Single());
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(PHIEUNHAPKHO pnkho)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            } 
            else
            {
                try
                {
                    ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
                    data.PHIEUNHAPKHOes.Add(pnkho);
                    SANPHAM sanpham = data.SANPHAMs.Single(n => n.MASP == pnkho.MASP);
                    sanpham.SOLUONG = sanpham.SOLUONG + pnkho.SOLUONG;
                    data.SaveChanges();
                    return RedirectToAction("Index", "KhoHang");
                }

                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var kho = from k in data.PHIEUNHAPKHOes where k.MAPHIEUNK == id select k;
                ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
                return View(kho.Single());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
                PHIEUNHAPKHO kho = data.PHIEUNHAPKHOes.SingleOrDefault(n => n.MAPHIEUNK == id);
                SANPHAM sanpham = data.SANPHAMs.SingleOrDefault(n => n.MASP == kho.MASP);
                var lstkho = from k in data.PHIEUNHAPKHOes where k.MASP == sanpham.MASP select k;
                UpdateModel(kho);
                sanpham.SOLUONG = 0;
                foreach (var item in lstkho)
                {
                    sanpham.SOLUONG = sanpham.SOLUONG + item.SOLUONG;
                }
                data.SaveChanges();
                return RedirectToAction("Index", "KhoHang");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var kho = from k in data.PHIEUNHAPKHOes where k.MAPHIEUNK == id select k;
                return View(kho.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                PHIEUNHAPKHO kho = data.PHIEUNHAPKHOes.SingleOrDefault(n => n.MAPHIEUNK == id);
                data.PHIEUNHAPKHOes.Remove(kho);
                SANPHAM sanpham = data.SANPHAMs.Single(n => n.MASP == kho.MASP);
                if (sanpham.SOLUONG > kho.SOLUONG)
                {
                    sanpham.SOLUONG = sanpham.SOLUONG - kho.SOLUONG;
                }
                else
                {
                    return RedirectToAction("ThongBao", "KhoHang");
                }
                data.SaveChanges();
                return RedirectToAction("Index", "KhoHang");
            }
        }
        public ActionResult ThongBao()
        {
            return View();
        }



    }
}