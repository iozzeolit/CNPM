using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cnpm.Models.Object;
public class HocVien
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string TenDangNhap { get; set; } = string.Empty;
    
    [Required]
    [StringLength(64)]
    public string MatKhau { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string HoTen { get; set; } = string.Empty;
    
    public DateTime NgaySinh { get; set; }
    
    [StringLength(100)]
    public string DiaChi { get; set; } = string.Empty;
    
    public int KhoiLop { get; set; }
}