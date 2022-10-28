using DinoPetStore.EF;
using DinoPetStore.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Antlr.Runtime;
using System.Drawing.Printing;

namespace DinoPetStore.Controllers
{
    public class UserController : Controller
    {
        DinoStoreDbContext data = new DinoStoreDbContext();

        public object CommonConstants { get; private set; }
        // GET: User
        public ActionResult Index()
        {

            var allacc = (from a in data.SANPHAMs
                          join b in data.THUONGHIEUx on a.MATH equals b.MATH
                          join c in data.LOAIs on a.MALOAI equals c.MALOAI
                          join d in data.MAUSACs on a.MAMAUSAC equals d.MAMAUSAC

                          select new ProductViewModel
                          {
                              MASP = a.MASP,
                              TENSP = a.TENSP,
                              DONGIABAN = a.DONGIABAN,
                              HINHANH = a.HINHANH,
                              MATH = a.MATH,
                              MALOAI = a.MALOAI,
                              TENTH = b.TENTH,
                              TENLOAI = c.TENLOAI,
                              SOLUONG = (int)a.SOLUONG,
                              MOTA = a.MOTA,
                              TENMAUSAC = d.TENMAUSAC,
                              LOGO = b.LOGO
                          }).OrderBy(x => x.MASP).Take(count: 6).ToList();
            return View(allacc);


        }


        public JsonResult GetListItemByCategory(int categoryId)
        {
            var result = (from a in data.SANPHAMs
                          join b in data.THUONGHIEUx on a.MATH equals b.MATH
                          join c in data.LOAIs on a.MALOAI equals c.MALOAI
                          join t in data.DANHMUCs on a.CategoryId equals t.CategoryId
                          join d in data.MAUSACs on a.MAMAUSAC equals d.MAMAUSAC
                          where (t.CategoryId == categoryId)
                          select new ProductViewModel
                          {
                              MASP = a.MASP,
                              TENSP = a.TENSP,
                              DONGIABAN = a.DONGIABAN,
                              HINHANH = a.HINHANH,
                              MATH = a.MATH,
                              MALOAI = a.MALOAI,
                              TENTH = b.TENTH,
                              TENLOAI = c.TENLOAI,
                              SOLUONG = (int)a.SOLUONG,
                              MOTA = a.MOTA,
                              TENMAUSAC = d.TENMAUSAC,
                              LOGO = b.LOGO
                          }).OrderBy(x => x.MASP).Take(count: 6).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetListItemDiscount()
        {
            try
            {
                var result = (from a in data.SANPHAMs
                              join g in data.GIAMGIAs on a.MASP equals g.MASP
                              join b in data.THUONGHIEUx on a.MATH equals b.MATH
                              join c in data.LOAIs on a.MALOAI equals c.MALOAI
                              join d in data.MAUSACs on a.MAMAUSAC equals d.MAMAUSAC

                              select new ProductViewModel
                              {
                                  MASP = a.MASP,
                                  TENSP = a.TENSP,
                                  DONGIABAN = a.DONGIABAN,
                                  HINHANH = a.HINHANH,
                                  MATH = a.MATH,
                                  MALOAI = a.MALOAI,
                                  TENTH = b.TENTH,
                                  TENLOAI = c.TENLOAI,
                                  SOLUONG = (int)a.SOLUONG,
                                  MOTA = a.MOTA,
                                  TENMAUSAC = d.TENMAUSAC,
                                  LOGO = b.LOGO
                              }).OrderBy(x => x.MASP).ToList();

                return Json(result, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }


        }

        public ActionResult GiamGia (int? page, int? pageSize)
        {

            var giamgia = (from a in data.SANPHAMs
                           join g in data.GIAMGIAs on a.MASP equals g.MASP
                           join b in data.THUONGHIEUx on a.MATH equals b.MATH
                           join c in data.LOAIs on a.MALOAI equals c.MALOAI
                           join d in data.MAUSACs on a.MAMAUSAC equals d.MAMAUSAC

                           select new ProductViewModel
                           {
                               MASP = a.MASP,
                               TENSP = a.TENSP,
                               DONGIABAN = a.DONGIABAN,
                               HINHANH = a.HINHANH,
                               MATH = a.MATH,
                               MALOAI = a.MALOAI,
                               TENTH = b.TENTH,
                               TENLOAI = c.TENLOAI,
                               SOLUONG = (int)a.SOLUONG,
                               MOTA = a.MOTA,
                               TENMAUSAC = d.TENMAUSAC,
                               LOGO = b.LOGO
                           }).OrderBy(x => x.MASP).ToList();

            return View(giamgia);
        }


        #region Lấy Sản Phẩm
        public ActionResult sanpham(int? page, int? pageSize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 5;
            }
            int pagenum = (page ?? 1);

            var allacc = (from a in data.SANPHAMs
                          join b in data.THUONGHIEUx on a.MATH equals b.MATH
                          join c in data.LOAIs on a.MALOAI equals c.MALOAI
                          join d in data.MAUSACs on a.MAMAUSAC equals d.MAMAUSAC

                          select new ProductViewModel
                          {
                              MASP = a.MASP,
                              TENSP = a.TENSP,
                              DONGIABAN = a.DONGIABAN,
                              HINHANH = a.HINHANH,
                              MATH = a.MATH,
                              MALOAI = a.MALOAI,
                              TENTH = b.TENTH,
                              TENLOAI = c.TENLOAI,
                              SOLUONG = (int)a.SOLUONG,
                              MOTA = a.MOTA,
                              TENMAUSAC = d.TENMAUSAC,
                              LOGO = b.LOGO
                          }).OrderBy(x => x.MASP);
            return View(allacc.ToPagedList(pagenum, (int)pageSize));
        }
        #endregion

        public ActionResult hinhanh(int id)
        {
            var listhinhanh = from HINH in data.HINHs where HINH.MASP == id select HINH;
            return PartialView(listhinhanh);
        }

        public ActionResult listhinhanhnho(int id)
        {
            var listhinhanhnho = from HINH in data.HINHs where HINH.MASP == id select HINH;
            return PartialView(listhinhanhnho);
        }
        public ActionResult listhinhanhnhoduoi(int id)
        {
            var listhinhanhnho = from HINH in data.HINHs where HINH.MASP == id select HINH;
            return PartialView(listhinhanhnho);
        }

        #region Lấy chi tiết sản phẩm
        public ActionResult Chitiet(int id)
        {
            //Mã Sp 1027 không tồn tại trong bảng hình HINH
            var detail = from a in data.SANPHAMs
                         join b in data.THUONGHIEUx on a.MATH equals b.MATH
                         join c in data.LOAIs on a.MALOAI equals c.MALOAI
                         join d in data.MAUSACs on a.MAMAUSAC equals d.MAMAUSAC
                         join h in data.HINHs on a.MASP equals h.MASP into hi
                         from g in hi.DefaultIfEmpty()
                         where a.MASP == id
                         select new ProductViewModel
                         {
                             MASP = a.MASP,
                             TENSP = a.TENSP,
                             DONGIABAN = a.DONGIABAN,
                             HINHANH = a.HINHANH,
                             MATH = a.MATH,
                             MALOAI = a.MALOAI,
                             TENTH = b.TENTH,
                             TENLOAI = c.TENLOAI,
                             SOLUONG = (int)a.SOLUONG,
                             MOTA = a.MOTA,
                             TENMAUSAC = d.TENMAUSAC,
                             HINH1 = g.HINH1 == null ? "" : g.HINH1,//Sử dujnh leftjoin thay vì join
                             THANHTOANON = a.THANHTOANON
                         }; 
            return View(detail.FirstOrDefault());
        }
        #endregion

        public ActionResult gioithieu()
        {
            return View();
        }

        public ActionResult loai()
        {
            var listloai = from LOAI in data.LOAIs select LOAI;
            return PartialView(listloai);
        }

        #region Lấy kích thước sản phẩm
        public ActionResult kichthuoc()
        {
            var listkichthuoc = from KICHTHUOC in data.KICHTHUOCs select KICHTHUOC;
            return PartialView(listkichthuoc);
        }
        #endregion

        #region Lấy thương hiệu sản phẩm
        public ActionResult thuonghieu()
        {
            var listthuonghieu = from THUONGHIEU in data.THUONGHIEUx select THUONGHIEU;
            return PartialView(listthuonghieu);
        }
        #endregion


        #region Lấy sản phẩm theo loại sản phẩm
        public  ActionResult SPTheoloai(int maloai, int? page, int? pageSize)
        {
            var sanpham = (from a in data.SANPHAMs
                           where a.MALOAI == maloai
                           select a).OrderBy(id => id.MALOAI);

            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 6;
            }
            int pagenum = (page ?? 1);
            return View(sanpham.ToPagedList(pagenum, (int)pageSize));
        }
        #endregion
// tham số truyền vào của SPTheoloai và SPTheothuonghieu chưa đc

        #region Lấy sản phẩm theo thương hiệu
        public  ActionResult SPTheothuonghieu(int math,int? page, int? pageSize)
        {

            var sanpham = (from a in data.SANPHAMs.AsNoTracking()
                           where a.MATH == math
                           select a).OrderBy(id => id.MATH);

            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 6;
            }
            int pagenum = (page ?? 1);
            return View(sanpham.ToPagedList(pagenum, (int)pageSize));
        }
        #endregion




        #region Đăng Ký
        public ActionResult dangky()
        {
            return View();
        }

        //Kiem tra tendn

        public bool kiemtratendn(string tendn)
        {
            return data.KHACHHANGs.Count(x => x.TENDNKH == tendn) > 0;
        }

        public bool kiemtraemail(string email)
        {
            return data.KHACHHANGs.Count(x => x.EMAIL == email) > 0;
        }

        [HttpPost]
        public ActionResult dangky(DangKyModel model)
        {
            if (ModelState.IsValid)
            {
                if (kiemtratendn(model.tendn))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if (kiemtraemail(model.email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var mahoa_matkhau = MahoaMD5.EncryptMD5(model.matkhau);
                    var kh = new KHACHHANG();
                    kh.HOTENKH = model.hoten;
                    kh.TENDNKH = model.tendn;
                    kh.MATKHAUKH = mahoa_matkhau;
                    kh.EMAIL = model.email;
                    kh.DIACHI = model.diachi;
                    kh.DIENTHOAI = model.dienthoai;
                    kh.HINHANH = model.hinhanh;
                    kh.NGAYSINH = model.ngaysinh;
                    data.KHACHHANGs.Add(kh);
                    data.SaveChanges();
                    return RedirectToAction("dangnhap");
                }
            }
            return View(model);
        }

        #endregion

        #region Đăng nhập tài khoản người dùng
        public ActionResult dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult dangnhap(DangNhapModel model)
        {
            var mahoa_matkhaudangnhap = MahoaMD5.EncryptMD5(model.matkhau);
            if (ModelState.IsValid)
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.TENDNKH == model.tendn && n.MATKHAUKH == mahoa_matkhaudangnhap);
                if (kh != null)
                {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("index", "User");
                }
                else
                    ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
            }
            else
            {

            }
            return View(model);
        }

        #endregion

        #region Kiểm tra thông tin cá nhân của tài khoản đăng nhập
        public ActionResult thongtincanhan()
        {
            if (Session["Taikhoan"] == null)
            {
                return RedirectToAction("Index", "User");

            }
            return View();
        }
        #endregion


        #region Sửa thông tin cá nhân khách hàng
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (Session["Taikhoan"] == null)
                return RedirectToAction("index", "User");
            else
            {
                var thongtin = from tt in data.KHACHHANGs where tt.MAKH == id select tt;
                return View(thongtin.Single());
            }
        }
        #endregion


        #region Cập nhật thông tin cá nhân người dùng
        [HttpPost, ActionName("Edit")]
        [ValidateInput(false)]
        public ActionResult Capnhat(int id, HttpPostedFileBase fileUpload)
        {
            if (Session["Taikhoan"] == null)
                return RedirectToAction("index", "User");
            else
            {
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.MAKH == id);
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
                    kh.HINHANH = fileName;
                    Session["Taikhoan"] = kh;
                    UpdateModel(kh);
                    data.SaveChanges();
                    return RedirectToAction("thongtincanhan", "User");
                }
            }
        }
        #endregion

        #region Đăng xuất tài khoản
        public ActionResult dangxuat()
        {
            Session.Clear();
            return RedirectToAction("Index", "User");
        }
        #endregion


        #region Tìm kiếm sản phẩm
        public ActionResult Ketquatimkiem(string searchString)
        {

            var links = from l in data.SANPHAMs
                        select l;

            if (!String.IsNullOrEmpty(searchString)) /*Nếu không phải trống thì lấy ra sản phẩm có tên với từ khóa tìm kiếm*/
            {
                links = links.Where(s => s.TENSP.Contains(searchString));
            }
            //Trả về tất cả sản phẩm
            return View(links);
        }
        #endregion


        #region Lấy kích thước sản phẩm theo mã sản phẩm
        public ActionResult KTtheoMaSP(int id)
        {
            var KichThuoc = from KICHTHUOC in data.KICHTHUOCs where KICHTHUOC.MASP == id select KICHTHUOC;
            return PartialView(KichThuoc);
        }
        #endregion


        #region Gửi liên kết xác minh thay đổi mật khẩu về mail khách hàng yêu cầu
        [NonAction]
        public void SendVerificationLinkEmail(string emailId, string activationCode, string emailFor = "ResetPassword")
        {
            var verifyUrl = "/User/" + emailFor + "/" + activationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);

            var fromEmail = new MailAddress("thanh170120@outlook.com.vn", "DinoStore"); //mail của mình để gửi mail đổi mật khẩu cho khách
            var toEmail = new MailAddress(emailId);
            var fromEmailPassword = "Thanh30072020"; //Mật khẩu của tài khoản mail
            string subject = "";
            string body = "";
            if (emailFor == "ResetPassword")
            {
                subject = "Đặt lại mật khẩu";
                body = "<b>Xin chào bạn</b>,<br/><br/> Chúng tôi đã nhận được yêu cầu đặt lại mật khẩu của bạn. Vui lòng nhấp vào liên kết dưới đây để thiết lập mật khẩu mới cho tài khoản của bạn " + "<br/><br/><a href=" + link + ">Link đặt lại mật khẩu</a>";
            }


            var smtp = new SmtpClient
            {
                Host = "smtp.office365.com",
                Port = 25,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
            };

            using (var message = new MailMessage(fromEmail, toEmail)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            }) smtp.Send(message);
        }
        #endregion


        [HttpGet]
        public ActionResult QuenMK()
        {
            return View();
        }


        #region Chức năng quên mật khẩu
        [HttpPost]
        public ActionResult QuenMK(QuenMKModel quenMK)
        {
            if (ModelState.IsValid)
            {
                //Xác nhận email
                //tạo liên kết đặt lại mật khẩu
                //gửi email               

                var account = data.KHACHHANGs.SingleOrDefault(n => n.EMAIL == quenMK.Email);
                if (account != null)
                {
                    //gửi mail để thay đổi mật khẩu

                    string resetCode = Guid.NewGuid().ToString();
                    SendVerificationLinkEmail(account.EMAIL, resetCode, "ResetPassword");
                    account.KHOIPHUCMATKHAU = resetCode;
                    data.SaveChanges();
                    return RedirectToAction("QuenMKxacnhan", "User");

                }
                else
                {

                    ViewBag.message = "Email không đúng";
                }

            }
            else
            {

            }
            return View(quenMK);
        }
        #endregion


        public ActionResult ResetPassword(string id)
        {
            //xác nhận liên kết đặt lại mật khẩu
            //tìm tài khoản được chỉ định với liên kết này
            //chuyển hướng để đặt lại trang mật khẩu
            KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.KHOIPHUCMATKHAU == id);
            if (kh != null)
            {
                ResetPassword model = new ResetPassword();
                model.Resetcode = id;
                return View(model);
            }
            else
            {
                return HttpNotFound();
            }
        }


        #region Thay đổi mật khẩu thành công
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            var message = "";
            if (ModelState.IsValid)
            {
                var mahoa_matkhau = MahoaMD5.EncryptMD5(model.NewPassword);
                KHACHHANG kh = data.KHACHHANGs.SingleOrDefault(n => n.KHOIPHUCMATKHAU == model.Resetcode);
                if (kh != null)
                {
                    kh.MATKHAUKH = mahoa_matkhau;
                    kh.KHOIPHUCMATKHAU = "";
                    UpdateModel(kh);
                    data.SaveChanges();
                    message = "Cập nhật mật khẩu mới thành công ";
                    return RedirectToAction("dangnhap", "User");
                }
            }
            else
            {
                message = "Điều gì đó không hợp lệ";

            }
            ViewBag.Message = message;
            return View(model);
        }
        #endregion


        public ActionResult QuenMKxacnhan()
        {
            return View();
        }

        public ActionResult thongbaolienhe()
        {
            return View();
        }


        [NonAction]
        public void sendcontact(string name, string email, string Subject, string message)
        {
           try
            {
                var fromEmail = new MailAddress("thanh170120@outlook.com.vn", "DinoStore"); //mail của mình để gửi mail đổi mật khẩu cho khách
                var toEmail = new MailAddress(email);
                var fromEmailPassword = "Thanh30072020"; //Mật khẩu của tài khoản mail
                string body = "<br/> Họ tên: " + name + "<br/><br/> Email: " + " " + email + "<br/><br/> Nội dung: " + message;

                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 25,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                };

                using (var mess = new MailMessage(fromEmail, toEmail)
                {
                    Subject = Subject,
                    Body = body,
                    IsBodyHtml = true
                }) smtp.Send(mess);

            }
            catch(Exception ex)
            {
                var exxx = ex.Message;
            }

        }



        [HttpGet]
        public ActionResult Lienhe()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Lienhe(LienheModel lienhe)
        {
            if (ModelState.IsValid)
            {
                var objData = new LIENHE
                {
                    Email = lienhe.Email,
                    Title = lienhe.Subject,
                    ContentContact = lienhe.Message,
                    RequestBy = lienhe.Name,
                    RequestDate = DateTime.Now,
                    IsResponse = true
                };
                data.LIENHEs.Add(objData);
                data.SaveChanges();
                return RedirectToAction("thongbaolienhe", "User");  
            }
            return View(lienhe);
        }

        public ActionResult TinTuc()
        {
            return View();
        }
    }
}
