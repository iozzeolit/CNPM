using System.ComponentModel.DataAnnotations;

namespace Cnpm.Models.Object;

public class IdCounter
{
    [Key]
    [StringLength(50)]
    public string TableName { get; set; } = string.Empty; 
    
    public int CurrentId { get; set; } = 0;
}