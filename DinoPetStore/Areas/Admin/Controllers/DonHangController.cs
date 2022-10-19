using DinoPetStore.App_Start;
using DinoPetStore.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

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


        public ActionResult ChiTietDonHang(int id, int? page, int? pageSize)
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
                    pageSize = 10;
                }
                //Lấy ra thông tin Chi tiết đơn hàng từ mã đơn hàng truyền vào
                //Ở đây 1 đơn hàng có thể có nhiều chi tiết ĐH(mua nhiều SP), nên dùng where như trang sản phẩm theo nhà sản xuất
                var CTDH = (from c in data.CTDONDATHANGs where c.MADH == id select c).ToList();
                return View(CTDH.ToPagedList((int) page, (int) pageSize));
            }
        }

    }
}