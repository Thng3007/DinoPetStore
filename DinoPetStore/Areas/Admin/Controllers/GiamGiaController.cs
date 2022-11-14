using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinoPetStore.App_Start;
using DinoPetStore.EF;
using PagedList;

namespace DinoPetStore.Areas.Admin.Controllers
{

    [AdminPhanQuyen(MACHUCNANG = "QL_KHOSANPHAM")]
    public class GiamGiaController : Controller
    {
        DinoStoreDbContext data = new DinoStoreDbContext();

        // GET: Admin/GiamGia
        public ActionResult Index(int? page, int? pagesize)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                if (page == null)
                {
                    page = 1;
                }
                if (pagesize == null)
                {
                    pagesize = 5;
                }

                var giamgia = data.GIAMGIAs.ToList();
                return View(giamgia.ToPagedList((int)page, (int)pagesize));

            }
        }

        public JsonResult getGiam()
        {
            var giamgia = (from a in data.GIAMGIAs.AsNoTracking()
                           join b in data.SANPHAMs.AsNoTracking() on a.MASP equals b.MASP
                           select new
                           {
                               a.MAGIAMGIA,
                               a.MASP,
                               b.TENSP,
                               a.PHAMTRAMGIAM,
                               a.TUNGAY,
                               a.DENNGAY
                           }).ToList();
            return Json(giamgia, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
                return View();
            }
        }
        [HttpPost]
        public ActionResult Create(GIAMGIA giamgia)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
                data.GIAMGIAs.Add(giamgia);
                data.SaveChanges();
                return RedirectToAction("Index", "KichThuoc");

            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var giamza = from kt in data.GIAMGIAs where kt.MAGIAMGIA == id select kt;
                return View(giamza.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                GIAMGIA giamza = data.GIAMGIAs.SingleOrDefault(n => n.MAGIAMGIA == id);
                data.GIAMGIAs.Remove(giamza);
                data.SaveChanges();
                return RedirectToAction("Index", "KichThuoc");
            }
        }
    }
}