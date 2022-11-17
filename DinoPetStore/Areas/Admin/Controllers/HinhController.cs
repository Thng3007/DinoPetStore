using DinoPetStore.App_Start;
using DinoPetStore.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinoPetStore.Areas.Admin.Controllers
{
    [AdminPhanQuyen(MACHUCNANG = "QL_HINHMOTA")]
    public class HinhController : Controller
    {
            DinoStoreDbContext data = new DinoStoreDbContext();
            // GET: Hinh
            public ActionResult Index(int? page, int? pagesize)
            {
                if (Session["Taikhoanadmin"] == null)
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
                            pagesize = 10;   
                        }

                var hink = data.HINHs.ToList();
                return View(hink.ToPagedList((int)page, (int)pagesize));

                }
            }
            public ActionResult Details(int id)
            {
                if (Session["Taikhoanadmin"] == null)
                    return RedirectToAction("dangnhap", "Admin");
                else
                {
                    var hinh = from h in data.HINHs where h.MAHINH == id select h;
                    if (hinh == null)
                    {
                        Response.StatusCode = 404;
                        return null;
                    }
                    return View(hinh.Single());
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
            public ActionResult Create(HINH hinh, HttpPostedFileBase fileUpload)
            {
                if (Session["Taikhoanadmin"] == null)
                    return RedirectToAction("dangnhap", "Admin");
                else
                {
                    ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
                    if (fileUpload == null)
                    {
                        ViewBag.Thongbao = "Vui lòng chọn ảnh bìa";
                        return View();
                    }
                    else
                    {
                        var fileName = Path.GetFileName(fileUpload.FileName);
                        var path = Path.Combine(Server.MapPath("~/img/"), fileName);
                        if (System.IO.File.Exists(path))
                            ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                        else
                        {
                            fileUpload.SaveAs(path);
                        }
                        hinh.HINH1 = fileName;
                        data.HINHs.Add(hinh);
                        data.SaveChanges();
                        return RedirectToAction("Index", "Hinh");
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
                    var hinh = from h in data.HINHs where h.MAHINH == id select h;

                    ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
                    return View(hinh.Single());
                }
            }
            [HttpPost, ActionName("Edit")]
            public ActionResult Capnhat(int id, HttpPostedFileBase fileUpload)
            {
                if (Session["Taikhoanadmin"] == null)
                    return RedirectToAction("dangnhap", "Admin");
                else
                {
                    HINH hinh = data.HINHs.SingleOrDefault(g => g.MAHINH == id);
                    ViewBag.MASP = new SelectList(data.SANPHAMs.ToList().OrderBy(n => n.TENSP), "MASP", "TENSP");
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
                        hinh.HINH1 = fileName;
                        UpdateModel(hinh);
                        data.SaveChanges();
                        return RedirectToAction("Index", "Hinh");
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
                    var hinh = from h in data.HINHs where h.MAHINH == id select h;
                    return View(hinh.Single());
                }
            }
            [HttpPost, ActionName("Delete")]
            public ActionResult Xoa(int id)
            {
                if (Session["Taikhoanadmin"] == null)
                    return RedirectToAction("dangnhap", "Admin");
                else
                {
                    HINH hinh = data.HINHs.SingleOrDefault(g => g.MAHINH == id);
                    data.HINHs.Remove(hinh);
                    data.SaveChanges();
                    return RedirectToAction("Index", "Hinh");
                }
            }

            public JsonResult getHinh()
            {
            var hinh = (from h in data.HINHs
                        join s in data.SANPHAMs on h.MASP equals s.MASP
                        select new
                        {
                            h.MAHINH,
                            h.HINH1,
                            h.ANHIEN,
                            s.MASP,
                            s.TENSP
                        }).ToList();
            return Json(hinh, JsonRequestBehavior.AllowGet);
            }


            public JsonResult deleteHinh(int id) {

                if (Session["Taikhoanadmin"] == null)
                    return Json("forgetsession", JsonRequestBehavior.AllowGet);
                else
                {
                    HINH hinh = data.HINHs.SingleOrDefault(g => g.MAHINH == id);
                    data.HINHs.Remove(hinh);
                    data.SaveChanges();
                    return Json("success", JsonRequestBehavior.AllowGet);
                }
        }


    }
}