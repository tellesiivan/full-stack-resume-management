namespace backend.Core.Models;

public abstract class BaseSearchQuery
{
    public bool IsDescending { get; set; } = false;
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}