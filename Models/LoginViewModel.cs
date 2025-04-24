using System.ComponentModel.DataAnnotations;

namespace Cnpm.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
    [Display(Name = "Tên đăng nhập")]
    public string TenDangNhap { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    [Display(Name = "Mật khẩu")]
    [DataType(DataType.Password)]
    public string MatKhau { get; set; } = string.Empty;
    
    [Display(Name = "Ghi nhớ đăng nhập")]
    public bool RememberMe { get; set; }
}