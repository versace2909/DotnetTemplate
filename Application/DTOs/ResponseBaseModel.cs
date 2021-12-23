namespace Application.DTOs;

public class BaseModel
{
    public string ErrorCode { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
    public List<string> Errors { get; set; }
}

public class ResponseBaseModel : BaseModel
{
    public static ResponseBaseModel Fail(string errorMessage)
    {
        return new ResponseBaseModel()
        {
            ErrorCode = "",
            Success = false,
            Message = string.Empty,
            Errors = new List<string>() {errorMessage}
        };
    }

    public static ResponseBaseModel Fail(List<string> errors)
    {
        return new ResponseBaseModel()
        {
            ErrorCode = "",
            Success = false,
            Message = string.Empty,
            Errors = errors
        };
    }
}

public class ResponseBaseModel<T> : BaseModel
{
    public T Data { get; set; }

    public static ResponseBaseModel<T> Succeed(T data)
    {
        return new ResponseBaseModel<T>()
        {
            ErrorCode = "",
            Success = true,
            Message = "",
            Data = data
        };
    }

    public static ResponseBaseModel<T> Succeed(T data, string message)
    {
        return new ResponseBaseModel<T>()
        {
            ErrorCode = "",
            Success = true,
            Message = message,
            Data = data
        };
    }

    public static ResponseBaseModel<T?> Fail(string message)
    {
        return new ResponseBaseModel<T?>()
        {
            ErrorCode = "",
            Success = false,
            Message = message,
            Data = default(T)
        };
    }
}

public class ListResponseBaseModel<T> : BaseModel
{
    public IEnumerable<T> Datas { get; set; }

    public static ListResponseBaseModel<T> Succeed(IEnumerable<T> datas)
    {
        return new ListResponseBaseModel<T>()
        {
            ErrorCode = "",
            Success = true,
            Message = "",
            Datas = datas
        };
    }
}