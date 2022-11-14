using DinoPetStore.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.IO;
using System.Web.UI.WebControls;
using System.Drawing.Printing;
using System.Web.UI;
using PagedList;

namespace DinoPetStore.Areas.Admin.Controllers
{
    public class ThuongHieuController : Controller
    {
        // GET: Admin/ThuongHieu
        DinoStoreDbContext db = new DinoStoreDbContext();

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

                var th = db.THUONGHIEUx.ToList();
                return View(th.ToPagedList((int)page, (int)pagesize));
            }
        }

        public JsonResult getListTH()
        {
            var TH = (from c in db.THUONGHIEUx
                      select new
                        {
                            c.MATH,
                            c.TENTH,
                            c.LOGO
                        }).ToList();
            return Json(TH, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(THUONGHIEU thieu, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                if(fileUpload == null)
                {
                    ViewBag.ThongBao = "Vui lòng chọn ảnh bìa";
                    return View();
                }
                else
                {
                    if (ModelState.IsValid)
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/img/"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.ThongBao = "Hình ảnh đã tồn tại";
                        }
                        else
                        {
                            fileUpload.SaveAs(path);
                        }

                        thieu.LOGO = fileName;
                        db.THUONGHIEUx.Add(thieu);
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index", "ThuongHieu");

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
                var thieu = from thuonghieu in db.THUONGHIEUx where thuonghieu.MATH == id select thuonghieu;
                return View(thieu.Single());
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                var thieu = from thuonghieu in db.THUONGHIEUx where thuonghieu.MATH == id select thuonghieu;
                return View(thieu.Single());
            }
        }


        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {

                THUONGHIEU loai = db.THUONGHIEUx.SingleOrDefault(n => n.MATH == id);

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
                    loai.LOGO = fileName;
                    UpdateModel(loai);
                    db.SaveChanges();
                    return RedirectToAction("Index", "ThuongHieu");
                }
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                var thieu = from thuonghieu in db.THUONGHIEUx where thuonghieu.MATH == id select thuonghieu;
                return View(thieu.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                THUONGHIEU thhieu = db.THUONGHIEUx.SingleOrDefault(n => n.MATH == id);
                db.THUONGHIEUx.Remove(thhieu);
                db.SaveChanges();
                return RedirectToAction("Index", "ThuongHieu");
            }
        }

    }
}