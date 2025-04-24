using System.ComponentModel.DataAnnotations.Schema;

namespace Cnpm.Models.Object;
public class DayHoc
{
    [ForeignKey("GiaSu")]
    public int IdGiaSu { get; set; }
    
    [ForeignKey("KhoaHoc")]
    public int IdKhoaHoc { get; set; }
}