using DinoPetStore.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DinoPetStore.EF;
using System.IO;
using System.Web.Helpers;

namespace DinoPetStore.Areas.Admin.Controllers
{

    [AdminPhanQuyen(MACHUCNANG = "QL_KICHTHUOC")]
    public class KichThuocController : Controller
    {

        DinoStoreDbContext data = new DinoStoreDbContext();
        // GET: Admin/KichThuoc
        public ActionResult Index(int? page, int? pagesize )
        {
            if (Session["Taikhoanadmin"] == null )
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                if(page == null)
                {
                    page = 1;
                }
                if(pagesize == null)    
                {
                    pagesize = 5;
                }
                var kt = data.KICHTHUOCs.ToList();
                return View(kt.ToPagedList((int)page, (int)pagesize));
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
                var kichthuoc = data.KICHTHUOCs.AsNoTracking().FirstOrDefault(c=>c.MAKICHTHUOC == id);
                if (kichthuoc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }

                return View(kichthuoc);
            }
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
        public ActionResult Create(KICHTHUOC kichthuoc)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
                data.KICHTHUOCs.Add(kichthuoc);
                data.SaveChanges();
                return RedirectToAction("Index", "KichThuoc");

            }
        }

        public ActionResult Edit(int Id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");


            ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
            var objData = data.KICHTHUOCs.AsNoTracking().FirstOrDefault(c => c.MAKICHTHUOC == Id);
            return View(objData);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult Capnhat(KICHTHUOC kichthuoc)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");

            var objData = data.KICHTHUOCs.FirstOrDefault(n => n.MAKICHTHUOC == kichthuoc.MAKICHTHUOC);
            objData.MASP = kichthuoc.MASP;
            objData.TENKICHTHUOC = kichthuoc.TENKICHTHUOC;
            data.SaveChanges();
            return RedirectToAction("Index", "KichThuoc");
        }
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var kichthuoc = from kt in data.KICHTHUOCs where kt.MAKICHTHUOC == id select kt;
                return View(kichthuoc.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                KICHTHUOC kichthuoc = data.KICHTHUOCs.SingleOrDefault(n => n.MAKICHTHUOC == id);
                data.KICHTHUOCs.Remove(kichthuoc);
                data.SaveChanges();
                return RedirectToAction("Index", "KichThuoc");
            }
        }



        public JsonResult getKichThuoc()
        {
            var kichthuoc = (from k in data.KICHTHUOCs
                             join l in data.SANPHAMs on k.MASP equals l.MASP
                             select new
                             {
                                 k.MAKICHTHUOC,
                                 k.MASP,
                                 l.TENSP,
                                 k.TENKICHTHUOC
                             }).ToList();
            return Json(kichthuoc, JsonRequestBehavior.AllowGet);
        }


        public JsonResult deleteKichThuoc(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return Json("forgetsession", JsonRequestBehavior.AllowGet);
            else
            {
                KICHTHUOC kichthuoc = data.KICHTHUOCs.SingleOrDefault(n => n.MAKICHTHUOC == id);
                data.KICHTHUOCs.Remove(kichthuoc);
                data.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
        }


    }
}