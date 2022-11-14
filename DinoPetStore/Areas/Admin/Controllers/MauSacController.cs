using DinoPetStore.App_Start;
using DinoPetStore.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace DinoPetStore.Areas.Admin.Controllers
{
    [AdminPhanQuyen(MACHUCNANG = "QL_MAUSAC")]
    public class MauSacController : Controller
    {
        // GET: Admin/MauSac

        DinoStoreDbContext db = new DinoStoreDbContext();
        public ActionResult Index(int? page, int? pagesize)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
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

                var ms = db.MAUSACs.ToList();
                return View(ms.ToPagedList((int)page, (int)pagesize));
            }
        }

        public JsonResult getMau()
        {
            var mau = (from l in db.MAUSACs
                        select new
                        {
                            l.MAMAUSAC,
                            l.TENMAUSAC,
                        }).ToList();
            return Json(mau, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var mau = from m in db.MAUSACs where m.MAMAUSAC == id select m;
                return View(mau.Single());
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(MAUSAC mau)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                db.MAUSACs.Add(mau);
                db.SaveChanges();
                return RedirectToAction("Index", "Mausac");
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var mau = from m in db.MAUSACs where m.MAMAUSAC == id select m;
                return View(mau.Single());
            }
        }

        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                MAUSAC mau = db.MAUSACs.SingleOrDefault(n => n.MAMAUSAC == id);
                UpdateModel(mau);
                db.SaveChanges();
                return RedirectToAction("Index", "Mausac");
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var mau = from m in db.MAUSACs where m.MAMAUSAC == id select m;
                return View(mau.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                MAUSAC mau = db.MAUSACs.SingleOrDefault(n => n.MAMAUSAC == id);
                db.MAUSACs.Remove(mau);
                db.SaveChanges();
                return RedirectToAction("Index", "Mausac");
            }
        }
    }
}
