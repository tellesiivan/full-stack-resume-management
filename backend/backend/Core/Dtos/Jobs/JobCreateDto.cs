using System.ComponentModel.DataAnnotations;
using backend.Core.Enums;

namespace backend.Core.Dtos.Jobs;

public class JobCreateDto
{
    [Required]
    [MinLength(4, ErrorMessage = "Title must be a min of 4 characters")]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MinLength(10, ErrorMessage = "Description must be a min of 10 characters")]
    public string Description { get; set; } = string.Empty;
    [Required]
    public JobLevel JobLevel { get; set; }
}