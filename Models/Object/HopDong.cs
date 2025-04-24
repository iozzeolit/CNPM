using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cnpm.Models.Object;
public class HopDong
{
    [Key]
    public int Id { get; set; }
    
    [ForeignKey("HocVien")]
    public int IdHocVien { get; set; }
    
    [ForeignKey("GiaSu")]
    public int IdGiaSu { get; set; }
    
    [ForeignKey("KhoaHoc")]
    public int IdKhoaHoc { get; set; }
    
    [StringLength(50)]
    public string TrangThai { get; set; } = string.Empty;
}