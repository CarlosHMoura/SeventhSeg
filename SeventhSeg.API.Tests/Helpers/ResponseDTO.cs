namespace SeventhSeg.API.Tests.Helpers;

public class ResponseDTO<T>
{
    public HttpResponseMessage? Status { get; set; }
    public IEnumerable<T>? ResultList { get; set; }
    public T? Result { get; set; }
}
