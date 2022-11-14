using DinoPetStore.App_Start;
using DinoPetStore.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Microsoft.Ajax.Utilities;
using System.Web.Helpers;

namespace DinoPetStore.Areas.Admin.Controllers
{
    [AdminPhanQuyen(MACHUCNANG = "QL_DONDATHANG")]
    public class DonHangController : Controller
    {
        DinoStoreDbContext data = new DinoStoreDbContext();

        public Double iTONGTIEN { set; get; }
        public Double tTONGTIEN { set; get; }

        // GET: Admin/DonHang
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

        public ActionResult DonDatHang(int? page, int? pageSize)
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
                if(pageSize == null)
                {
                    pageSize = 9;
                }
                var hang = data.DONDATHANGs.ToList();
                return View(hang.ToPagedList((int)page, (int)pageSize));
            }
        }

        public JsonResult getDH()
        {
            var donhang = ( from d in data.DONDATHANGs.AsNoTracking()
                            join e in data.KHACHHANGs.AsNoTracking() on d.MAKH equals e.MAKH
                            select new
                            {
                                d.MADH,
                                d.DATHANHTOAN,
                                d.NGAYDAT,
                                d.NGAYGIAO,
                                d.MAKH, 
                                e.HOTENKH,
                                d.TONGTIEN
                            }).ToList();
            return Json(donhang, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ChiTietDonHang(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {
                //Lấy ra thông tin Chi tiết đơn hàng từ mã đơn hàng truyền vào
                //Ở đây 1 đơn hàng có thể có nhiều chi tiết ĐH(mua nhiều SP), nên dùng where như trang sản phẩm theo nhà sản xuất

                ViewBag.MADH = id;
                return View();
            }

        }


        public JsonResult GetListOrder(int MADH) 
        {
            var CTDH = (from c in data.CTDONDATHANGs
                        join a in data.DONDATHANGs on c.MADH equals a.MADH
                        join i in data.SANPHAMs on c.MASP equals i.MASP
                        where c.MADH == MADH
                        select new
                        {
                            c.MADH,
                            i.MASP,
                            i.TENSP,
                            a.TONGTIEN,
                            c.SOLUONG,
                            i.HINHANH,
                            c.DONGIA,
                            c.THANHTIEN,
                            a.TRANGTHAI
                        }).ToList();
            return Json(CTDH, JsonRequestBehavior.AllowGet);
        }



        public JsonResult UpdateStatus(int MADH, string status)
        {
            try
            {
                string mess = "success";
                var objData = data.DONDATHANGs.FirstOrDefault(c => c.MADH == MADH);
                if (objData == null)
                {
                    mess = "Đơn hàng không tồn tại";
                }
                //Nếu trạng thái là từ chối sẽ k được phép cập nhật nưax
                if (objData.TRANGTHAI == "REJECT")
                {
                    mess = "Đơn hàng đã bị từ chối";
                }
                if (objData.TRANGTHAI == "SUCCESS")
                {
                    mess = "Đơn hàng đã được giao thành công!";
                }

                if (objData.TRANGTHAI == "RETURN")
                {
                    mess = "Đơn hàng đã bị trả về";
                }

                if (objData.TRANGTHAI == "DELEVERY" && (status == "REJECT" || status == "RETURN"))
                {
                    mess = "Đơn hàng đang được giao không được phép hủy";
                }

                objData.TRANGTHAI = status;
                data.SaveChanges();
                return Json(mess, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

    }
}