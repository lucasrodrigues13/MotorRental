using MotorRental.Domain.Constants;
using Newtonsoft.Json;

namespace MotorRental.Application.Common
{
    public class ApiResponse
    {
        public ApiResponse(bool success, string message, object? data, List<string>? errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public bool Success { get; }
        public string Message { get; }
        public object? Data { get; }
        public List<string> Errors { get; }

        public static ApiResponse Ok(object? data = null, string message = ErrorMessagesConstants.SUCCESS_OK)
        {
            return new ApiResponse(true, message, data);
        }

        public static ApiResponse BadRequest(List<string> errors, string message = ErrorMessagesConstants.BADREQUEST_DEFAULT)
        {
            return new ApiResponse(false, message, default, errors);
        }
    }
}
