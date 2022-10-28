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

        public JsonResult StatisticsSaleByMonth()
        {
            try
            {
                var lstData = (from a in data.DONDATHANGs.AsNoTracking()
                               where a.TINHTRANGDH == true && a.NGAYDAT.Year == DateTime.Now.Year
                               group a by new { a.NGAYDAT.Month, a.NGAYDAT.Year } into g
                               select new
                               {
                                   g.Key.Month,
                                   g.Key.Year,
                                   TotalAmout = g.Sum(c => c.TONGTIEN)
                               }).ToList();
                return Json(lstData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public JsonResult CountOrderByMonth()
        {
            try
            {
                var lstData = (from a in data.DONDATHANGs.AsNoTracking()
                               where a.TINHTRANGDH == true && a.NGAYDAT.Year == DateTime.Now.Year
                               group a by new { a.NGAYDAT.Month, a.NGAYDAT.Year } into g
                               select new
                               {
                                   g.Key.Month,
                                   g.Key.Year,
                                   TotalOrder = g.Count()
                               }).ToList();
                return Json(lstData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult CountCustomOrder()
        {
            try
            {
                var lstCusOrder = data.DONDATHANGs.AsNoTracking().Select(c => c.MAKH).Distinct().ToList();

                var lstCustomer = data.KHACHHANGs.AsNoTracking().Count();

                var setRate = lstCustomer == 0 ? 0 : Math.Round(((double)lstCusOrder.Count() / (double)lstCustomer) * 100, 2);

                var lstData = new List<CountCustomOrderModel>
                {
                    new CountCustomOrderModel
                    {
                        Name = "Đã đặt",
                        Ratio = setRate
                    },
                    new CountCustomOrderModel
                    {
                         Name = "Chưa đặt",
                        Ratio = 100 - setRate
                    }
                };

                return Json(lstData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
public class CountCustomOrderModel
{
    public string Name { get; set; }
    public double Ratio { get; set; }
}