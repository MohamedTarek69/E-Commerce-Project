using E_Commerce.Shared.CommonResult;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiBaseController : ControllerBase
    {
        // Handle Result Without Value
        // If Result is Success => return NoContent 204
        // If Result is Failure => return ProblemDetails with StatusCode and Error Message
        protected IActionResult HandleResult(Result result)
        {
            if (result.IsSuccess)
                return NoContent();
            else
                return HandleProblem(result.Errors);
        }



        // Handle Result With Value
        // If Result is Success => return Ok with Value 200
        // If Result is Failure => return ProblemDetails with StatusCode and Error Message
        protected ActionResult HandleResult<TValue>(Result<TValue> result)
        {
            if (result.IsSuccess)
                return Ok(result.Value);
            else
                return HandleProblem(result.Errors);
        }

        private ActionResult HandleProblem(IReadOnlyList<Error> errors)
        {
            // If No Errors Are Provided, return 500 Internal Server Error
            if(errors == null || errors.Count == 0)
            {
                return Problem(
                    title: "General.Unknown",
                    detail: "An Unknown Error Occurred",
                    type: ErrorType.Unknown.ToString(),
                    statusCode: StatusCodes.Status500InternalServerError
                    );
            }
            // If There Are Multiple Errors, Handle It As a Validation Problem
            if(errors.All(e => e.ErrorType == ErrorType.Validation))
            {
                return HandleValidationProblem(errors);
            }
            // If There Is Only one Error , Handle It As a Single Error Problem
            return HandleSingleErrorProblem(errors[0]);


        }

        private ActionResult HandleSingleErrorProblem(Error error)
        {
            return Problem(
                title: error.Code,
                detail: error.Description,
                type: error.ErrorType.ToString(),
                statusCode: MapErrorTypeToStatusCode(error.ErrorType)
                );
        }
        private static int MapErrorTypeToStatusCode(ErrorType errorType) => errorType switch
        {
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            ErrorType.Forbidden => StatusCodes.Status403Forbidden,
            ErrorType.InvalidCredentials => StatusCodes.Status401Unauthorized,
            ErrorType.InternalServerError => StatusCodes.Status500InternalServerError,
            ErrorType.BadRequest => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.ServiceUnavailable => StatusCodes.Status503ServiceUnavailable,
            ErrorType.Timeout => StatusCodes.Status504GatewayTimeout,
            ErrorType.Failure => StatusCodes.Status500InternalServerError,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Unknown => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError,
        };

        private ActionResult HandleValidationProblem(IReadOnlyList<Error> errors)
        {
            var modelState = new ModelStateDictionary();
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Code, error.Description);
            }
            return ValidationProblem(modelState);

        }
    }
}
