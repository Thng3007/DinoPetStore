using DinoPetStore.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinoPetStore.Areas.Admin.Controllers
{
    public class ThongKeController : Controller
    {
        DinoStoreDbContext data = new DinoStoreDbContext();
        // GET: Admin/ThongKe
        public ActionResult Index()
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                ViewBag.TongDoanhThu = ThongKeDoanhThu();
                ViewBag.TongDonHang = ThongKeDonHang();
                ViewBag.TongSanPham = ThongKeSanPham();
                ViewBag.TongKhachHang = ThongKeKhachHang();
                ViewBag.TongAdmin = ThongKeNhanVien();
            }
            return View();
        }

        public double ThongKeDonHang()
        {
            double slDonHang = data.DONDATHANGs.Count();
            return slDonHang;
        }

        public double ThongKeSanPham()
        {
            double slSanPham = data.SANPHAMs.Count();
            return slSanPham;
        }

        public double ThongKeKhachHang()
        {
            double slKhachHang = data.KHACHHANGs.Count();
            return slKhachHang;
        }

        public double ThongKeNhanVien()
        {
            double slNhanVien = data.ADMINs.Count();
            return slNhanVien;
        }

        public decimal ThongKeDoanhThu()
        {
            decimal TongDoanhThu = decimal.Parse(data.CTDONDATHANGs.Sum(n => n.SOLUONG * n.DONGIA).ToString());
            return TongDoanhThu;
        }

        public decimal ThongKeDoanhThuThang(int Thang, int Nam)
        {
            var listDH = data.DONDATHANGs.Where(n => n.NGAYDAT.Month == Thang && n.NGAYDAT.Year == Nam);
            decimal TongTien = 0;
            foreach (var item in listDH)
            {
                TongTien += decimal.Parse(item.CTDONDATHANGs.Sum(n => n.SOLUONG * n.DONGIA).ToString());
            }
            return TongTien;
        }
    }
}