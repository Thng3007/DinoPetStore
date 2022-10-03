using DinoPetStore.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinoPetStore.Areas.Admin.Controllers
{
    public class AdminPQController : Controller
    {
        DinoStoreDbContext db = new DinoStoreDbContext();
        #region Phan Quyen       
        // GET: Admin/AdminPQ
        public ActionResult DSPhanQuyen()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                var ad = from admin in db.ADMINs select admin;
                return View(ad);
            }
        }

        public ActionResult ChiTietDSPhanQuyen(int id)
        {

            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                var ad = from admin in db.PHANQUYENs where admin.MAADMIN == id select admin;
                return View(ad);
            }
        }

        public ActionResult DSQuyen()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                var ad = from admin in db.CHUCNANG_QUYEN  select admin;
                return View(ad);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                ViewBag.MACHUCNANG = new SelectList(db.CHUCNANG_QUYEN.ToList().OrderBy(n => n.TENCN), "MACHUCNANG", "TENCN");
                ViewBag.MAADMIN = new SelectList(db.ADMINs.ToList().OrderBy(n => n.HOTEN), "MAADMIN", "HOTEN");
                return View();
            }
        }

        [HttpPost]
        public ActionResult Create(PHANQUYEN pquyen)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                ViewBag.MACHUCNANG = new SelectList(db.CHUCNANG_QUYEN.ToList().OrderBy(n => n.TENCN), "MACHUCNANG", "TENCN");
                ViewBag.MAADMIN = new SelectList(db.ADMINs.ToList().OrderBy(n => n.HOTEN), "MAADMIN", "HOTEN");
                db.PHANQUYENs.Add(pquyen);
                db.SaveChanges();
                return RedirectToAction("DSPhanQuyen", "AdminPQ");

            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var mau = from m in db.PHANQUYENs where m.MAPQ == id select m;
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
                PHANQUYEN mau = db.PHANQUYENs.SingleOrDefault(n => n.MAPQ == id);
                db.PHANQUYENs.Remove(mau);
                db.SaveChanges();
                return RedirectToAction("DSPhanQuyen", "AdminPQ");
            }
        }
#endregion

        #region CHỨC NĂNG QUYỀN
        public ActionResult DSChucNang()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");

            }
            else
            {
                var ad = from admin in db.CHUCNANG_QUYEN select admin;
                return View(ad);
            }
        }

        public ActionResult CreateCN()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                return View();
            }
        }

      
        #endregion
    

}
}