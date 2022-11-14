using DinoPetStore.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DinoPetStore.EF;
using DinoPetStore.App_Start;
using System.IO;
using System.Data.Linq;
using System.Security.Cryptography;
using Newtonsoft.Json.Serialization;

namespace DinoPetStore.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        DinoStoreDbContext data = new DinoStoreDbContext();
        // GET: Admin/Admin
        public ActionResult Index()
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

        [HttpGet]
        public ActionResult dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult dangnhap(DangNhapModel model)
        {
            if (ModelState.IsValid)
            {
                ADMIN ad = data.ADMINs.SingleOrDefault(n => n.TENDN == model.tendn && n.MATKHAU == model.matkhau);
                if (ad != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công !!";
                    Session["Taikhoanadmin"] = ad;
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu sai !!";
                }
            }
            else
            {

            }

            return View(model);
        }

        public ActionResult dangxuat()
        {
            Session.Clear();
            return RedirectToAction("dangnhap", "Admin");
        }

        public ActionResult thongtinadmin()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            return View();
        }

        [AdminPhanQuyen(MACHUCNANG = "QL_QUANTRIVIEN")]
        public ActionResult listadmin()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                var ad = from admin in data.ADMINs select admin;
                return View(ad);
            }
        }

        public JsonResult getListAdmin()
        {
            var listAdmin = (from a in data.ADMINs
                             select new
                             {
                                 a.HOTEN,
                                 a.DIENTHOAI,
                                 a.TENLOAI,
                                 a.TENDN,
                                 a.DIACHI,
                                 a.EMAIL,
                                 a.AVATAR
                             }).ToList();
            return Json(listAdmin, JsonRequestBehavior.AllowGet);
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
        public ActionResult Create(ADMIN admin, HttpPostedFileBase fileUp)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                if (ModelState.IsValid) 
                {
                    try
                    {
                        var fileName = Path.GetFileName(fileUp.FileName);
                        var path = Path.Combine(Server.MapPath("~/img/"), fileName);
                        if (System.IO.File.Exists(path))
                        {
                            ViewBag.Thongbao = "Hình Ảnh Đã Được Sử Dụng Hoặc Tồn Tại";
                        }
                        else
                        {
                            fileUp.SaveAs(path);
                        }
                        admin.AVATAR = fileName; 
                        data.ADMINs.Add(admin);
                        data.SaveChanges();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                    }


                }

                return RedirectToAction("listadmin", "Admin");
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
                var admin = from ad in data.ADMINs where ad.MAADMIN == id select ad;
                return View(admin.Single());
            }
        }


        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult Capnhat(int id, HttpPostedFileBase fileUp)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                ADMIN ad = data.ADMINs.SingleOrDefault(n => n.MAADMIN == id);
                if(fileUp == null )
                {
                    ViewBag.ThongBao = ("Vui lòng chọn ảnh bìa !!");
                    return View();
                }
                else
                {
                    var fileName = Path.GetFileName(fileUp.FileName);
                    var path = Path.Combine(Server.MapPath("~/img/"), fileName);
                    fileUp.SaveAs(path);
                    ad.AVATAR = fileName;
                    UpdateModel(ad);
                    data.SaveChanges();
                    return RedirectToAction("listadmin", "Admin");
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
                var ad = from adm in data.ADMINs where adm.MAADMIN == id select adm ;
                return View(ad.Single());
            }
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa (int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                ADMIN ad = data.ADMINs.SingleOrDefault(n => n.MAADMIN == id);
                data.ADMINs.Remove(ad);
                data.SaveChanges();
                return RedirectToAction("listadmin", "Admin");
            }
        }


        [HttpGet]
        public ActionResult DoiMK(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                DoiMKadmin model = new DoiMKadmin();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DoiMK(int id, DoiMKadmin changePass)
        {
            var mess = "";
            var pass_md5 = MahoaMD5.GetMD5(changePass.NewPass);
            if (ModelState.IsValid)
            {
                ADMIN ad = data.ADMINs.SingleOrDefault(n => n.MAADMIN == id && n.MATKHAU == pass_md5);
                if (ad != null)
                {
                    var pass_mahoa = MahoaMD5.GetMD5(changePass.NewPass);
                    ad.MATKHAU = pass_mahoa;
                    UpdateModel(ad);
                    Session["Taikhoanadmin"] = ad;
                    data.SaveChanges();
                    mess = "Cập nhật mật khẩu thành công";
                    return RedirectToAction("thongtinadmin", "Admin");
                }
                else
                {
                    ViewBag.ThongBao = "Mật khẩu cũ không đúng !!";
                }
            }
            else
            {
                mess = "Có lỗi không hợp lệ";
            }
            ViewBag.Message = mess;
            return View(changePass);
            
        }



 

    }
}