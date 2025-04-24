using System.ComponentModel.DataAnnotations.Schema;

namespace Cnpm.Models.Object;
public class TGBGiaSu
{
    [ForeignKey("GiaSu")]
    public int IdGiaSu { get; set; }
    
    public DateTime TDiemBatDau { get; set; }
    
    public DateTime TDiemKetThuc { get; set; }
}