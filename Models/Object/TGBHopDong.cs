using System.ComponentModel.DataAnnotations.Schema;

namespace Cnpm.Models.Object;
public class TGBHopDong
{
    [ForeignKey("HopDong")]
    public int IdHopDong { get; set; }
    
    public DateTime TDiemBatDau { get; set; }
    
    public DateTime TDiemKetThuc { get; set; }
}