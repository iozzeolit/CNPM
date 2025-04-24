using System.ComponentModel.DataAnnotations;

namespace Cnpm.Models;

public class StudentRegisterViewModel
{
    [Required(ErrorMessage = "Tên đăng nhập là bắt buộc")]
    [Display(Name = "Tên đăng nhập")]
    [StringLength(50, ErrorMessage = "Tên đăng nhập không được vượt quá 50 ký tự")]
    public string TenDangNhap { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Mật khẩu là bắt buộc")]
    [Display(Name = "Mật khẩu")]
    [DataType(DataType.Password)]
    [StringLength(64, ErrorMessage = "Mật khẩu không được vượt quá 64 ký tự")]
    public string MatKhau { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Xác nhận mật khẩu là bắt buộc")]
    [Display(Name = "Xác nhận mật khẩu")]
    [DataType(DataType.Password)]
    [Compare("MatKhau", ErrorMessage = "Mật khẩu và xác nhận mật khẩu không khớp")]
    public string XacNhanMatKhau { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Họ tên là bắt buộc")]
    [Display(Name = "Họ tên")]
    [StringLength(50, ErrorMessage = "Họ tên không được vượt quá 50 ký tự")]
    public string HoTen { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Ngày sinh là bắt buộc")]
    [Display(Name = "Ngày sinh")]
    [DataType(DataType.Date)]
    public DateTime NgaySinh { get; set; }
    
    [Display(Name = "Địa chỉ")]
    [StringLength(100, ErrorMessage = "Địa chỉ không được vượt quá 100 ký tự")]
    public string DiaChi { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Khối lớp là bắt buộc")]
    [Display(Name = "Khối lớp")]
    [Range(1, 12, ErrorMessage = "Khối lớp phải từ 1 đến 12")]
    public int KhoiLop { get; set; }
}