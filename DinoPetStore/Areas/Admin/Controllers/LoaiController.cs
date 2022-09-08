using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinoPetStore.App_Start;
using PagedList;
using DinoPetStore.EF;

namespace DinoPetStore.Areas.Admin.Controllers
{
    [AdminPhanQuyen( MACHUCNANG = "QL_LOAISANPHAM")]
    public class LoaiController : Controller
    {
        DinoStoreDbContext data = new DinoStoreDbContext();
        // GET: Admin/Loai
        public ActionResult Index(int? page, int? pagesize)
        {
            if(page == null)
            {
                page = 1;   
            }
            if(pagesize == null)
            {
                pagesize = 10;
            }
            var loai = data.LOAIs.ToList();
            return View(loai.ToPagedList((int)page, (int)pagesize));

        }

        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var loai = from l in data.LOAIs where l.MALOAI == id select l;
                return View(loai.Single());
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
        public ActionResult Create(LOAI loai)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                data.LOAIs.Add(loai);
                data.SaveChanges();
                return RedirectToAction("Index", "Loai");
            }
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var loai = from l in data.LOAIs where l.MALOAI == id select l;
                return View(loai.Single());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                LOAI loai = data.LOAIs.SingleOrDefault(n => n.MALOAI == id);
                UpdateModel(loai);
                data.SaveChanges();
                return RedirectToAction("Index", "Loai");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var loai = from nxb in data.LOAIs where nxb.MALOAI == id select nxb;
                return View(loai.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                LOAI nhaxuatban = data.LOAIs.SingleOrDefault(n => n.MALOAI == id);
                data.LOAIs.Remove(nhaxuatban);
                data.SaveChanges();
                return RedirectToAction("Index", "Loai");
            }
        }
    }


}
