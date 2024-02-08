namespace backend.Core.Models;

public class BaseResponse
{
    public bool IsSuccess { get; set; } = true;
    public string Message { get; set; } = string.Empty;
}