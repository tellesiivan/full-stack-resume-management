using System.ComponentModel.DataAnnotations;

namespace backend.Core.Entities;

// abstract can only be inherited
public class BaseEntity
{
    [Key]
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
    public bool IsActive { get; set; } = true;
}