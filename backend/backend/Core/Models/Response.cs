using System.Net;

namespace backend.Core.Models;

public class Response<TR>
{
    public TR? Data { get; set; }
    public bool IsSuccess { get; set; } = true;
    public string ErrorMessage { get; set; } = string.Empty;
    public HttpStatusCode? StatusCode { get; set; }
}