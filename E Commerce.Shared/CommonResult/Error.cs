using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Error
    {
        public string Code { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ErrorType ErrorType { get; set; }
        private Error(string code, string description, ErrorType errorType)
        {
            Code = code;
            Description = description;
            ErrorType = errorType;
        }
        // Static Factory Methods To Create Error
        #region Static Factory Methods To Create Error
        public static Error Failure(string code = "General.Failure", string description = "Failure Has Occured")
        {
            return new Error(code, description, ErrorType.Failure);
        }
        public static Error Validation(string code = "General.Validation", string description = "Validation Error Occured")
        {
            return new Error(code, description, ErrorType.Validation);
        }
        public static Error NotFound(string code = "General.NotFound", string description = "Resource Not Found")
        {
            return new Error(code, description, ErrorType.NotFound);
        }
        public static Error Unauthorized(string code = "General.Unauthorized", string description = "Unauthorized Access")
        {
            return new Error(code, description, ErrorType.Unauthorized);
        }
        public static Error Forbidden(string code = "General.Forbidden", string description = "Access Forbidden")
        {
            return new Error(code, description, ErrorType.Forbidden);
        }
        public static Error InvalidCredentials(string code = "General.InvalidCredentials", string description = "Invalid Credentials Provided")
        {
            return new Error(code, description, ErrorType.InvalidCredentials);
        }
        public static Error InternalServerError(string code = "General.InternalServerError", string description = "An Internal Server Error Has Occurred")
        {
            return new Error(code, description, ErrorType.InternalServerError);
        }
        public static Error BadRequest(string code = "General.BadRequest", string description = "Bad Request")
        {
            return new Error(code, description, ErrorType.BadRequest);
        }
        public static Error Conflict(string code = "General.Conflict", string description = "Conflict Occurred")
        {
            return new Error(code, description, ErrorType.Conflict);
        }
        public static Error ServiceUnavailable(string code = "General.ServiceUnavailable", string description = "Service Unavailable")
        {
            return new Error(code, description, ErrorType.ServiceUnavailable);
        }
        public static Error Timeout(string code = "General.Timeout", string description = "Request Timed Out")
        {
            return new Error(code, description, ErrorType.Timeout);
        }
        public static Error Unknown(string code = "General.Unknown", string description = "An Unknown Error Occurred")
        {
            return new Error(code, description, ErrorType.Unknown);
        }

        #endregion
    }
}
