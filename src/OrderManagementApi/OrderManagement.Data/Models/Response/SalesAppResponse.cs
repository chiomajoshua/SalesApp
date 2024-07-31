// Ignore Spelling: App

using OrderManagement.Data.Constants;

namespace OrderManagement.Data.Models.Response;

public class SalesAppResponse
{
    public bool Status { get; set; }
    public int ResponseCode { get; set; }
    public string? ResponseMessage { get; set; } = null;
    public object? ResponseData { get; set; } = null;

    public static SalesAppResponse SuccessResponse(object? data = null)
    {
        return new SalesAppResponse
        {
            Status = true,
            ResponseData = data,
            ResponseCode = ApiStatusConstants.OK
        };
    }

    public static SalesAppResponse ConflictResponse(string? message = null)
    {
        return new SalesAppResponse
        {
            Status = false,
            ResponseMessage = message,
            ResponseCode = ApiStatusConstants.Conflict
        };
    }

    public static SalesAppResponse CreatedResponse(object? data = null)
    {
        return new SalesAppResponse
        {
            Status = true,
            ResponseCode = ApiStatusConstants.Created,
            ResponseData = data
        };
    }

    public static SalesAppResponse UnAuthorizedResponse()
    {
        return new SalesAppResponse
        {
            Status = false,
            ResponseCode = ApiStatusConstants.Unauthorized
        };
    }

    public static SalesAppResponse BadRequestResponse(object data, string? message = null)
    {
        return new SalesAppResponse
        {
            Status = false,
            ResponseData = data,
            ResponseCode = ApiStatusConstants.BadRequest,
            ResponseMessage = message
        };
    }

    public static SalesAppResponse ErrorResponse(string errorMessage)
    {
        return new SalesAppResponse
        {
            Status = false,
            ResponseMessage = errorMessage,
            ResponseCode = ApiStatusConstants.InternalServerError,
            ResponseData = errorMessage
        };
    }

    public static SalesAppResponse CustomExistsResponse<T>(T data)
    {
        bool isSuccess = data is not null;
        int statusCode = isSuccess ? ApiStatusConstants.OK : ApiStatusConstants.NotFound;

        return new SalesAppResponse
        {
            Status = isSuccess,
            ResponseCode = statusCode,
            ResponseMessage = isSuccess ? "Record(s) Found" : "No record(s) found",
            ResponseData = data,
        };
    }
}