using System.ComponentModel.DataAnnotations;

namespace Cnpm.Models.Object;

public class Admin
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string TenDangNhap { get; set; } = string.Empty;
    [Required]
    [StringLength(64)]
    public string MatKhau { get; set; } = string.Empty;
}