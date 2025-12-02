using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Result
    {
        private readonly List<Error> errors = [];
        public bool IsSuccess => errors.Count == 0;
        public bool IsFailure => !IsSuccess;
        public IReadOnlyList<Error> Errors => errors.AsReadOnly();

        #region Constructors
        // Ok - Success
        protected Result()
        {

        }
        // Fail With One Error
        protected Result(Error error)
        {
            errors.Add(error);
        }
        // Fail With Multiple Errors
        protected Result(List<Error> errors)
        {
            this.errors.AddRange(errors);
        }

        #endregion

        #region Factory Methods
        // Static Factory Methods
        // Ok - Success
        public static Result Ok() => new Result();
        // Fail With One Error
        public static Result Fail(Error error) => new Result(error);
        // Fail With Multiple Errors
        public static Result Fail(List<Error> errors) => new Result(errors);

        #endregion

    }
}
