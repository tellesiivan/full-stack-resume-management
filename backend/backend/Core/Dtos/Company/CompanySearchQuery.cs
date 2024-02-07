using backend.Core.Enums;
using backend.Core.Models;

namespace backend.Core.Dtos.Company;

public class CompanySearchQuery: BaseSearchQuery
{
    public CompanySize? Size { get; set; }
    public string? CompanyName { get; set; }
}