using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DinoPetStore.Areas.Admin.Models
{
    public class DangNhapModel
    {
        [Display(Name = "Tên Đăng Nhập: ")]
        [Required(ErrorMessage = "Tên Đăng Nhập Không Được Để Trống !!")]
        public string tendn { set; get; }

        [Display(Name = "Mật khẩu:")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 tự. ")]
        [Required(ErrorMessage = "Mật khẩu không được để trống. ")]
        public string matkhau { set; get; }
    }
}