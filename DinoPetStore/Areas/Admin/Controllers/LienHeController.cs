using DinoPetStore.EF;
using System;
using PagedList;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.Net.Mail;
using System.Net;

namespace DinoPetStore.Areas.Admin.Controllers
{
    public class LienHeController : Controller
    {
        // GET: Admin/LienHe
        DinoStoreDbContext data = new DinoStoreDbContext();
        public ActionResult GetListContact(int? page, int? pageSize)
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
                if (pageSize == null)
                {
                    pageSize = 9;
                }
                var lienhe = data.LIENHEs.ToList();
                return View(lienhe.ToPagedList((int)page, (int)pageSize));
            }
        }
        public JsonResult ResponseContact(int Id, string contentResponse)
        {
            try
            {
                var objData = data.LIENHEs.FirstOrDefault(x => x.Id == Id && x.IsResponse);
                if (objData == null)
                {
                    return Json("Dữ liệu không tồn tại hoặc đã được phản hồi", JsonRequestBehavior.AllowGet);
                }
                else
                {
                    objData.IsResponse = false;
                    //Gui email
                    var fromEmail = new MailAddress("thanh170120@outlook.com.vn", "Thanh"); //mail của mình để gửi mail đổi mật khẩu cho khách
                    var toEmail = new MailAddress(objData.Email);
                    var fromEmailPassword = "Thanh30072020"; //Mật khẩu của tài khoản mail
                    string body = "Nội dung: " + contentResponse;
                    string subject = "Phản hồi từ DinoStore";
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
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    }) smtp.Send(mess);
                    data.SaveChanges();
                    return Json("success",JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

    }
}