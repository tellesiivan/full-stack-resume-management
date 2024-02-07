using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using backend.Core.Enums;
using Newtonsoft.Json.Converters;

namespace backend.Core.Dtos.Company;

public class CompanyCreationDto
{
    [Required]
    [MinLength(4, ErrorMessage = "Company name must be at least 4 characters long")]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public CompanySize Size { get; set; }
}