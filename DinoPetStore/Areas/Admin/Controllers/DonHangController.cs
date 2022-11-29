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
using System.Net.Mail;
using System.Net;

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

        public JsonResult deleteDH(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return Json("forgetsession", JsonRequestBehavior.AllowGet);
            }
            else
            {
                DONDATHANG donhang = data.DONDATHANGs.SingleOrDefault(n => n.MADH == id);
                data.DONDATHANGs.Remove(donhang);
                data.SaveChanges();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult ChiTietDonHang(int id)
        {
            if (Session["Taikhoanadmin"] == null)
            {
                return RedirectToAction("dangnhap", "Admin");
            }
            else
            {

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

                //Lấy danh sách sản phẩm
                var lstItem = (from a in data.SANPHAMs.AsNoTracking()
                               join b in data.CTDONDATHANGs.AsNoTracking() on a.MASP equals b.MASP
                               where b.MADH == MADH
                               select new
                               {
                                   a.TENSP,
                                   b.DONGIA,
                                   b.THANHTIEN
                               }).ToList();
                string tableItem = string.Empty;
                tableItem += "<table>\r\n  <tr>\r\n    <th>Tên SP</th>\r\n    <th>Đơn giá</th>\r\n    <th>Thành tiền</th>\r\n  </tr>";
                foreach (var item in lstItem)
                {
                    tableItem += $"<tr>\r\n    <td>{item.TENSP}</td>\r\n    <td>{item.DONGIA}</td>\r\n    <td>{item.THANHTIEN}</td>\r\n  </tr>";
                }
                tableItem += "</table>";
                //Gửi mail cho khách hàng
                var objCus = (from a in data.DONDATHANGs.AsNoTracking()
                              join b in data.KHACHHANGs.AsNoTracking() on a.MAKH equals b.MAKH
                              where a.MADH == MADH
                              select new { b.EMAIL }).FirstOrDefault();
                if (objCus == null)
                {
                    mess = "Không tìm thấy email khách hàng";
                }
                string strStatus = string.Empty;
                switch (status)
                {
                    case "REJECT":
                        strStatus = "Đơn hàng bị từ chối";
                        break;
                    case "APPROVAL":
                        strStatus = "Đơn hàng đã được xác nhận";
                        break;
                    case "DELIVERY":
                        strStatus = "Đơn hàng đang được vận chuyển";
                        break;
                    case "SUCCESS":
                        strStatus = "Đơn hàng đã được giao thành công";
                        break;
                    case "RETURN":
                        strStatus = "Đơn hàng đã được trả lại";
                        break;
                }
                var fromEmail = new MailAddress("thanh170120@outlook.com.vn", "DinoStore"); //mail của mình để gửi mail đổi mật khẩu cho khách
                var toEmail = new MailAddress(objCus.EMAIL);
                var fromEmailPassword = "Thanh30072020"; //Mật khẩu của tài khoản mail
                string body = $"<b>Mã đơn hàng</b> : {MADH} </br>";
                body += $"<b>Danh sách sản phẩm</b> : </br> {tableItem} </br>";
                body += $"<b>Trạng thái : </b> {strStatus}";
                string subject = "Phản hồi trạng thái đơn hàng từ DinoStore";
                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword),
                };

                using (var mail = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                }) smtp.Send(mail);


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