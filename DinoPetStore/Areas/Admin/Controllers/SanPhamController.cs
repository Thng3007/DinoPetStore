using DinoPetStore.App_Start;
using DinoPetStore.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinoPetStore.Areas.Admin.Controllers
{
    [AdminPhanQuyen(MACHUCNANG = "QL_SANPHAM")]
    public class SanPhamController : Controller
    {
        DinoStoreDbContext data = new DinoStoreDbContext();
        // GET: SanPham
        public ActionResult Index(int? page, int? pageSize)
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
                if(pageSize == null)
                {
                    pageSize = 9;
                }
                var sanpham = data.SANPHAMs.ToList();
                return View(sanpham.ToPagedList((int)page, (int)pageSize));
            }
        }
        public ActionResult Details(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                var sanpham = from g in data.SANPHAMs where g.MASP == id select g;
                if (sanpham == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                return View(sanpham.Single());
            }
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                ViewBag.MALOAI = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                ViewBag.MATH = new SelectList(data.THUONGHIEUx.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
                ViewBag.MAMAUSAC = new SelectList(data.MAUSACs.ToList().OrderBy(n => n.TENMAUSAC), "MAMAUSAC", "TENMAUSAC");
                return View();
            }
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection collection, SANPHAM sanpham, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                ViewBag.MALOAI = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                ViewBag.MATH = new SelectList(data.THUONGHIEUx.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
                ViewBag.MAMAUSAC = new SelectList(data.MAUSACs.ToList().OrderBy(n => n.TENMAUSAC), "MAMAUSAC", "TENMAUSAC");
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
                    sanpham.HINHANH = fileName;
                    data.SANPHAMs.Add(sanpham);
                    data.SaveChanges();
                    return RedirectToAction("Index", "SanPham");
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
                var sanpham = from g in data.SANPHAMs where g.MASP == id select g;

                ViewBag.MALOAI = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                ViewBag.MATH = new SelectList(data.THUONGHIEUx.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
                ViewBag.MAMAUSAC = new SelectList(data.MAUSACs.ToList().OrderBy(n => n.TENMAUSAC), "MAMAUSAC", "TENMAUSAC");
                return View(sanpham.Single());
            }
        }
        [HttpPost, ActionName("Edit")]
        public ActionResult Capnhat(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                SANPHAM sanpham = data.SANPHAMs.SingleOrDefault(g => g.MASP == id);
                ViewBag.MALOAI = new SelectList(data.LOAIs.ToList().OrderBy(n => n.TENLOAI), "MALOAI", "TENLOAI");
                ViewBag.MATH = new SelectList(data.THUONGHIEUx.ToList().OrderBy(n => n.TENTH), "MATH", "TENTH");
                ViewBag.MAMAUSAC = new SelectList(data.MAUSACs.ToList().OrderBy(n => n.TENMAUSAC), "MAMAUSAC", "TENMAUSAC");
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
                    sanpham.HINHANH = fileName;
                    UpdateModel(sanpham);
                    data.SaveChanges();
                    return RedirectToAction("Index", "SanPham");
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
                var sanpham = from g in data.SANPHAMs where g.MASP == id select g;
                return View(sanpham.Single());
            }
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xoa(int id)
        {
            if (Session["Taikhoanadmin"] == null)
                return RedirectToAction("dangnhap", "Admin");
            else
            {
                SANPHAM sanpham = data.SANPHAMs.SingleOrDefault(g => g.MASP == id);
                var kichthuoc = from KICHTHUOC in data.KICHTHUOCs where KICHTHUOC.MASP == id select KICHTHUOC;
                var hinh = from HINH in data.HINHs where HINH.MASP == id select HINH;
                var kho = from PHIEUNHAPKHO in data.PHIEUNHAPKHOes where PHIEUNHAPKHO.MASP == id select PHIEUNHAPKHO;
                var dathang = from CTDONDATHANG in data.CTDONDATHANGs where CTDONDATHANG.MASP == id select CTDONDATHANG;
                var dondathang = from DONDATHANG in data.DONDATHANGs select DONDATHANG;
                foreach (var item in dathang)
                {
                    data.CTDONDATHANGs.Remove(item);
                }
                /*foreach (var item in dathang)
                {
                    foreach (var itam in dondathang)
                    {
                        if (itam.MADH != item.MADH)
                        {
                            data.DONDATHANGs.DeleteOnSubmit(itam);
                        }
                    }
                }*/
                foreach (var item in hinh)
                {
                    data.HINHs.Remove(item);
                }
                foreach (var item in kho)
                {
                    data.PHIEUNHAPKHOes.Remove(item);
                }
                foreach (var item in kichthuoc)
                {
                    data.KICHTHUOCs.Remove(item);
                }
                data.SANPHAMs.Remove(sanpham);
                data.SaveChanges();
                return RedirectToAction("Index", "SanPham");
            }
        }

        #region UpdateProduct
        //Hàm Ẩn hoặc Hiện Sản phẩm (ở đây sử dụng hàm void để Response.Write hình update lại)
        [HttpPost]
        public void UpdateProduct(int id)
        {
            //Lấy ra sản phẩm cần Update Ẩn Hiện
            var _sp = (from s in data.SANPHAMs where s.MASP == id select s).SingleOrDefault();

            //Tạo chuỗi _Hinh để chưa đường dẫn hình Ẩn Hiện khi Update lại
            string _Hinh = "";

            //Ẩn thì cập nhật lại thành hiện và ngược lại
            if (_sp.ANHIEN == true)
            {
                _sp.ANHIEN = false;
                _Hinh = "/Content/Images/icon_An.png";
            }
            else
            {
                _sp.ANHIEN = true;
                _Hinh = "/Content/Images/icon_Hien.png";
            }

            //Lưu chỉnh sửa
            UpdateModel(_sp);
            data.SaveChanges();

            //Xuất ra (Trả về) đường dẫn hình để Update lại trên Form
            Response.Write(_Hinh);
        }


        public JsonResult SearchDataProduct(string fromDate, string toDate, string keyWord)
        {
            try
            {
                

                if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
                {
                    var _fromDate = string.IsNullOrEmpty(fromDate) ? DateTime.Now : DateTime.ParseExact(fromDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var _toDate = string.IsNullOrEmpty(toDate) ? DateTime.Now : DateTime.ParseExact(toDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    var sanpham = (from a in data.SANPHAMs.AsNoTracking()
                                   join b in data.THUONGHIEUx.AsNoTracking() on a.MATH equals b.MATH
                                   where a.NGAYTAO >= _fromDate && a.NGAYTAO <= _toDate
                                   select new
                                   {
                                       a.MASP,
                                       a.TENSP,
                                       a.DONGIAMUA,
                                       a.DONGIABAN,
                                       a.SOLUONG,
                                       a.MALOAI,
                                       a.MOTA,
                                       a.HINHANH,
                                       a.ANHIEN,
                                       b.TENTH
                                   }).ToList();
                    return Json(sanpham, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var sanpham = (from a in data.SANPHAMs.AsNoTracking()
                                   join b in data.THUONGHIEUx.AsNoTracking() on a.MATH equals b.MATH
                                   select new
                                   {
                                       a.MASP,
                                       a.TENSP,
                                       a.DONGIAMUA,
                                       a.DONGIABAN,
                                       a.SOLUONG,
                                       a.MALOAI,
                                       a.MOTA,
                                       a.HINHANH,
                                       b.TENTH,
                                       a.ANHIEN
                                   }).ToList();

                    return Json(sanpham, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}