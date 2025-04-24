using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cnpm.Models.Object;
public class KhoaHoc
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string TenKhoaHoc { get; set; } = string.Empty;
    
    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string TenMonHoc { get; set; } = string.Empty;
    
    public int KhoiLop { get; set; }
    
    public int SoBuoi { get; set; }
    
    public int HocPhi { get; set; }
}