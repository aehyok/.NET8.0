using System;
using System.ComponentModel.DataAnnotations;

namespace aehyok.Core.Model
{
    public class LoginModel
    {
        [Required(ErrorMessage = "请重新输入用户名。")]
        [Display(Name = "用户名")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "请重新输入密码。")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }
    }
}
