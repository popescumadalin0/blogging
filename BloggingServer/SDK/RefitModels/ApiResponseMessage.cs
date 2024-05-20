using System.Net;

namespace SDK.RefitModels;

public class ApiResponseMessage
{
    public ApiResponseMessage()
    {
    }

    public ApiResponseMessage(
        bool success,
        HttpStatusCode statusCode = HttpStatusCode.OK,
        string responseMessage = null,
        string reasonPhrase = null)
    {
        Success = success;
        ResponseMessage = responseMessage;
        StatusCode = (int)statusCode;
        ReasonPhrase = reasonPhrase;
    }

    public ApiResponseMessage(
        bool success,
        int statusCode,
        string responseMessage = null,
        string reasonPhrase = null)
    {
        Success = success;
        ResponseMessage = responseMessage;
        StatusCode = statusCode;
        ReasonPhrase = reasonPhrase;
    }

    public bool Success { get; set; }

    public string ResponseMessage { get; set; }

    public string ReasonPhrase { get; set; }

    public int StatusCode { get; set; }

    public string ClientError { get; set; }
}
public class ApiResponseMessage<T>
{
    public ApiResponseMessage(T response)
    {
        Response = response;
    }


    public ApiResponseMessage(bool success, T response, HttpStatusCode statusCode = HttpStatusCode.OK, string responseMessage = null, string reasonPhrase = null)
    {
        Success = success;
        ResponseMessage = responseMessage;
        StatusCode = (int)statusCode;
        ReasonPhrase = reasonPhrase;
        Response = response;
    }

    public ApiResponseMessage(bool success, T response, int statusCode, string responseMessage = null, string reasonPhrase = null)
    {
        Success = success;
        ResponseMessage = responseMessage;
        StatusCode = statusCode;
        ReasonPhrase = reasonPhrase;
        Response = response;
    }


    public bool Success { get; set; }

    public string ResponseMessage { get; set; }

    public string ReasonPhrase { get; set; }

    public int StatusCode { get; set; }

    public T Response { get; set; }

    public string ClientError { get; set; }
}