using System.Net;

namespace OrderManagement.Data.Constants;

public static class ApiStatusConstants
{
    public const int OK = (int)HttpStatusCode.OK; // 200
    public const int Created = (int)HttpStatusCode.Created; // 201
    public const int Accepted = (int)HttpStatusCode.Accepted; // 202
    public const int NoContent = (int)HttpStatusCode.NoContent; // 204
    public const int BadRequest = (int)HttpStatusCode.BadRequest; // 400
    public const int Unauthorized = (int)HttpStatusCode.Unauthorized; // 401
    public const int Forbidden = (int)HttpStatusCode.Forbidden; // 403
    public const int NotFound = (int)HttpStatusCode.NotFound; // 404
    public const int MethodNotAllowed = (int)HttpStatusCode.MethodNotAllowed; // 405
    public const int NotAcceptable = (int)HttpStatusCode.NotAcceptable; // 406
    public const int Conflict = (int)HttpStatusCode.Conflict; // 409
    public const int Gone = (int)HttpStatusCode.Gone; // 410
    public const int PreconditionFailed = (int)HttpStatusCode.PreconditionFailed; // 412
    public const int UnsupportedMediaType = (int)HttpStatusCode.UnsupportedMediaType; // 415
    public const int UnprocessableEntity = (int)HttpStatusCode.UnprocessableEntity; // 422
    public const int TooManyRequests = (int)HttpStatusCode.TooManyRequests; // 429
    public const int InternalServerError = (int)HttpStatusCode.InternalServerError; // 500
    public const int NotImplemented = (int)HttpStatusCode.NotImplemented; // 501
    public const int BadGateway = (int)HttpStatusCode.BadGateway; // 502
    public const int ServiceUnavailable = (int)HttpStatusCode.ServiceUnavailable; // 503
    public const int GatewayTimeout = (int)HttpStatusCode.GatewayTimeout; // 504
}