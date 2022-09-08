using DinoPetStore.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DinoPetStore.EF;
using System.IO;

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
                var kichthuoc = from k in data.KICHTHUOCs select k;
                if (kichthuoc == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }

                return View(kichthuoc.Single());
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
    }
}