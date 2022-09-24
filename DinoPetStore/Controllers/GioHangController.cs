﻿using DinoPetStore.Models;
using DinoPetStore.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DinoPetStore.Controllers
{
    public class GioHangController : Controller
    {
        DinoStoreDbContext data = new DinoStoreDbContext();
        // GET: Admin/GioHang


        //Xay dung trang Gio hang
        public ActionResult GioHang()
        {
            //List<GioHang> dsGiohang = LayGioHang();
            //if (dsGiohang.Count == 0)
            //{
            //    return RedirectToAction("Index", "User");
            //}
            //ViewBag.Tongsoluong = TongSoLuong();
            //ViewBag.Tongtien = TongTien();
            return View(/*dsGiohang*/);
        }

        public List<GioHang> LayGioHang()
        {
            List<GioHang> dsGioHang = Session["Giohang"] as List<GioHang>;
            if (dsGioHang == null)
            {
                dsGioHang = new List<GioHang>();
                Session["Giohang"] = dsGioHang;
            }
            return dsGioHang;
        }

        public ActionResult ThemGiohang(int iMASP, string strURL)
        {
            List<GioHang> dsGiohang = LayGioHang();
            GioHang sanpham = dsGiohang.Find(n => n.iMASP == iMASP);
            if (sanpham == null)
            {
                sanpham = new GioHang(iMASP);
                dsGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSOLUONG++;
                return Redirect(strURL);
            }
        }


        //Tong so luong
        private int TongSoLuong()
        {
            int iTongSoLuong = 0;
            List<GioHang> dsGiohang = Session["GioHang"] as List<GioHang>;
            if (dsGiohang != null)
            {
                iTongSoLuong = dsGiohang.Sum(n => n.iSOLUONG);
            }
            return iTongSoLuong;
        }
        //Tinh tong tien
        private double TongTien()
        {
            double iTongTien = 0;
            List<GioHang> dsGiohang = Session["GioHang"] as List<GioHang>;
            if (dsGiohang != null)
            {
                iTongTien = dsGiohang.Sum(n => n.dTHANHTIEN);
            }
            return iTongTien;
        }

        //Tao Partial view de hien thi thong tin gio hang
        public ActionResult GiohangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            return PartialView();
        }

        //Cap nhat Giỏ hàng
        public ActionResult CapnhatGiohang(int iMaSP, FormCollection f)
        {
            List<GioHang> dsGiohang = LayGioHang();
            GioHang sanpham = dsGiohang.SingleOrDefault(n => n.iMASP == iMaSP);
            if (sanpham != null)
            {
                sanpham.iSOLUONG = int.Parse(f["txtSoluong"].ToString());
            }
            return RedirectToAction("Giohang");
        }
        //Xoa Giohang
        public ActionResult XoaGiohang(int iMaSP)
        {
            List<GioHang> dsGiohang = LayGioHang();
            GioHang sanpham = dsGiohang.SingleOrDefault(n => n.iMASP == iMaSP);
            if (sanpham != null)
            {
                dsGiohang.RemoveAll(n => n.iMASP == iMaSP);
                return RedirectToAction("GioHang");

            }
            if (dsGiohang.Count == 0)
            {
                return RedirectToAction("index", "User");
            }
            return RedirectToAction("GioHang");
        }
        //Xoa tat ca thong tin trong Gio hang
        public ActionResult XoaTatcaGiohang()
        {
            List<GioHang> dsGiohang = LayGioHang();
            dsGiohang.Clear();
            return RedirectToAction("index", "User");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            //Kiem tra dang nhap
            if (Session["Taikhoan"] == null || Session["Taikhoan"].ToString() == "")
            {
                return RedirectToAction("Dangnhap", "User");
            }
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "User");
            }

            //Lay gio hang tu Session
            List<GioHang> lstGiohang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.TongTien = TongTien();

            return View(lstGiohang);
        }

        #region Thêm đơn đặt hàng mới
        [HttpPost]
        public ActionResult DatHang(FormCollection collection)
        {
            //Them Don hang
            DONDATHANG ddh = new DONDATHANG();
            KHACHHANG kh = (KHACHHANG)Session["Taikhoan"];
            List<GioHang> gh = LayGioHang();
            ddh.MAKH = kh.MAKH;
            ddh.NGAYDAT = DateTime.Now;
            if (collection["Ngaygiao"].Equals(""))
            {
                DateTime aDateTime = DateTime.Now;
                DateTime newTime = aDateTime.AddDays(7);
                ddh.NGAYGIAO = newTime;
            }
            else
            {
                var ngaygiao = String.Format("{0:dd/MM/yyyy}", collection["Ngaygiao"]);
                ddh.NGAYGIAO = DateTime.Parse(ngaygiao);
            }

            ddh.TINHTRANGDH = false;

            int HTTH = int.Parse(collection["sl_ThanhToan"]);
            if (HTTH == 0)
                ddh.DATHANHTOAN = false;
            else
                ddh.DATHANHTOAN = true;

            ddh.TONGTIEN = (decimal)TongTien();
            data.DONDATHANGs.Add(ddh);
            data.SaveChanges();
            foreach (var item in gh)
            {
                SANPHAM sanpham = data.SANPHAMs.Single(n => n.MASP == item.iMASP);
                if (sanpham.SOLUONG >= item.iSOLUONG)
                {
                    CTDONDATHANG ctdh = new CTDONDATHANG();
                    ctdh.MADH = ddh.MADH;
                    ctdh.MASP = item.iMASP;
                    ctdh.SOLUONG = item.iSOLUONG;
                    ctdh.DONGIA = (int)item.dDONGIA;
                    ctdh.THANHTIEN = (decimal)item.dTHANHTIEN;
                    data.CTDONDATHANGs.Add(ctdh);
                    sanpham.SOLUONG = sanpham.SOLUONG - item.iSOLUONG;
                    data.SaveChanges();
                    Session["GioHang"] = null;
                }
                else
                {
                    return RedirectToAction("ThongBao", "GioHang");
                }

            }
            return RedirectToAction("Xacnhandonhang", "GioHang");

        }
        #endregion



        public ActionResult ThongBao()
        {
            return View();
        }

        public ActionResult Xacnhandonhang()
        {
            var dh = from d in data.DONDATHANGs select d.NGAYGIAO;
            return View(dh);
        }

        #region Lấy hình thương hiệu
        public ActionResult hinhthuonghieu()
        {
            var listthuonghieu = from THUONGHIEU in data.THUONGHIEUx select THUONGHIEU;
            return PartialView(listthuonghieu);
        }
        #endregion
    }




}
