using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DinoPetStore.Areas.Admin.Models
{
    public class DoiMKadmin
    {
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 - 20 ký tự")]
        [Required(ErrorMessage = "Mật khẩu không được để trống !! ")]
        public string Checkpass { get; set; }


        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 - 20 ký tự")]
        [Required(ErrorMessage = "Mật khẩu không được để trống !! ")]
        public string NewPass { get; set; }


        [DataType(DataType.Password)]
        [Compare("NewPass", ErrorMessage = "Xác Nhận Mật Khẩu Không Khớp")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Độ dài mật khẩu ít nhất 6 - 20 ký tự")]
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống !! ")]
        public string ConfirmPass { get; set; }
    }
}