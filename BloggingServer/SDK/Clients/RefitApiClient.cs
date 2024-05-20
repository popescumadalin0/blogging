using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Refit;
using SDK.RefitModels;

namespace SDK.Clients;

public delegate void OnApiCallExecuted(ApiResponseMessage e);

public abstract class RefitApiClient<TU> where TU : class
{
    public event OnApiCallExecuted OnApiCallExecuted = delegate { };

    public async Task<ApiResponseMessage<T>> Execute<T>(Task<T> task)
    {
        try
        {
            var response = await task;
            OnApiCallExecuted(new ApiResponseMessage(true));
            return new ApiResponseMessage<T>(true, response);
        }
        catch (ApiException e)
        {
            var instance = default(T);
            try
            {
                instance = JsonConvert.DeserializeObject<T>(e.Content);
            }
            catch
            {
            }

            OnApiCallExecuted(new ApiResponseMessage
            {
                Success = false,
                StatusCode = (int)e.StatusCode,
                ResponseMessage = e.Content,
                ReasonPhrase = e.ReasonPhrase,
                ClientError = $"{e.Message}. Inner Exception: {e.InnerException?.Message}"
            });

            return new ApiResponseMessage<T>(instance)
            {
                Success = false,
                StatusCode = (int)e.StatusCode,
                ResponseMessage = e.Content,
                ReasonPhrase = e.ReasonPhrase,
                ClientError = $"{e.Message}. Inner Exception: {e.InnerException?.Message}"
            };
        }
        catch (Exception e)
        {
            OnApiCallExecuted(new ApiResponseMessage
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ResponseMessage = "SDK Exception",
                ReasonPhrase = "Unhandled Exception",
                ClientError = $"{e.Message}. Inner Exception: {e.InnerException?.Message}"
            });

            return new ApiResponseMessage<T>(default(T))
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ResponseMessage = "SDK Exception",
                ReasonPhrase = "Unhandled Exception",
                ClientError = $"{e.Message}. Inner Exception: {e.InnerException?.Message}"
            };
        }
    }

    public async Task<ApiResponseMessage> ExecuteWithNoContentResponse(Task task)
    {
        try
        {
            await task;
            OnApiCallExecuted(new ApiResponseMessage(true));
            return new ApiResponseMessage(true);
        }
        catch (ApiException e)
        {
            var result = new ApiResponseMessage
            {
                Success = false,
                StatusCode = (int)e.StatusCode,
                ResponseMessage = e.Content,
                ReasonPhrase = e.ReasonPhrase
            };
            OnApiCallExecuted(result);
            return result;
        }
        catch (Exception e)
        {
            var result = new ApiResponseMessage
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ResponseMessage = "SDK Exception",
                ReasonPhrase = "Unhandled Exception",
                ClientError = $"{e.Message}. Inner Exception: {e.InnerException?.Message}"
            };

            OnApiCallExecuted(result);
            return result;
        }
    }

    public async Task<ApiResponseMessage<T>> ExecuteAndTransform<T>(Task<object> task, Func<string, string> transformFunc)
    {
        try
        {
            var rawResponse = await task;
            var json = rawResponse.ToString();
            // Apply the transformation function to the JSON
            var transformedJson = transformFunc(json);
            var response = JsonConvert.DeserializeObject<T>(transformedJson);

            OnApiCallExecuted(new ApiResponseMessage(true));
            return new ApiResponseMessage<T>(true, response);
        }
        catch (ApiException e)
        {
            var instance = default(T);
            try
            {
                instance = JsonConvert.DeserializeObject<T>(e.Content);
            }
            catch
            {
            }

            OnApiCallExecuted(new ApiResponseMessage
            {
                Success = false,
                StatusCode = (int)e.StatusCode,
                ResponseMessage = e.Content,
                ReasonPhrase = e.ReasonPhrase,
                ClientError = $"{e.Message}. Inner Exception: {e.InnerException?.Message}"
            });

            return new ApiResponseMessage<T>(instance)
            {
                Success = false,
                StatusCode = (int)e.StatusCode,
                ResponseMessage = e.Content,
                ReasonPhrase = e.ReasonPhrase,
                ClientError = $"{e.Message}. Inner Exception: {e.InnerException?.Message}"
            };
        }
        catch (Exception e)
        {
            OnApiCallExecuted(new ApiResponseMessage
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ResponseMessage = "SDK Exception",
                ReasonPhrase = "Unhandled Exception",
                ClientError = $"{e.Message}. Inner Exception: {e.InnerException?.Message}"
            });

            return new ApiResponseMessage<T>(default(T))
            {
                Success = false,
                StatusCode = (int)HttpStatusCode.InternalServerError,
                ResponseMessage = "SDK Exception",
                ReasonPhrase = "Unhandled Exception",
                ClientError = $"{e.Message}. Inner Exception: {e.InnerException?.Message}"
            };
        }
    }
}