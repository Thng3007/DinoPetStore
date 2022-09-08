using DinoPetStore.Areas.Admin.Models;
using DinoPetStore.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using PagedList;

namespace DinoPetStore.Areas.Admin.Controllers
{
    public class KhachHangController : Controller
    {
        DinoStoreDbContext db = new DinoStoreDbContext();  


        // GET: KhachHang
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

                var kh = db.KHACHHANGs.ToList();
                return View(kh.ToPagedList((int)page, (int)pagesize));
                
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
                var kh = from khach in db.KHACHHANGs where khach.MAKH == id select khach;
                return View(kh.Single());
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
                return View();
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(KHACHHANG kh, HttpPostedFileBase fileUpLoad)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {

                if (fileUpLoad == null)
                {
                    ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var fileName = Path.GetFileName(fileUpLoad.FileName);
                        var path = Path.Combine(Server.MapPath("~/img/"), fileName);
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            fileUpLoad.SaveAs(path);
                        }
                        kh.HINHANH = fileName;
                        kh.MATKHAUKH = MahoaMD5.GetMD5(kh.MATKHAUKH);
                        db.KHACHHANGs.Add(kh);
                        db.SaveChanges();

                    }

                }
                return RedirectToAction("Index", "KhachHang");
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var kh = from khach in db.KHACHHANGs where khach.MAKH == id select khach;
                return View(kh.Single());
            }
        }
        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult Capnhat(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
                if (fileUpload == null)
                {
                    ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                else
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/img/"), fileName);
                    fileUpload.SaveAs(path);
                    kh.HINHANH = fileName;
                    UpdateModel(kh);
                    db.SaveChanges();
                    return RedirectToAction("Index", "KhachHang");
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var kh = from khach in db.KHACHHANGs where khach.MAKH == id select khach;
                return View(kh.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
                db.KHACHHANGs.Remove(kh);
                db.SaveChanges();
                return RedirectToAction("Index", "KhachHang");
            }
        }
    }




}
