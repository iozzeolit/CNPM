using System.ComponentModel.DataAnnotations.Schema;

namespace Cnpm.Models.Object;
public class TGBHocVien
{
    [ForeignKey("HocVien")]
    public int IdHocVien { get; set; }
    
    public DateTime TDiemBatDau { get; set; }
    
    public DateTime TDiemKetThuc { get; set; }
}