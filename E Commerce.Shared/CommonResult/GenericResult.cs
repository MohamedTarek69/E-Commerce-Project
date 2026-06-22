using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Shared.CommonResult
{
    public class Result<TValue> : Result
    {
        private readonly TValue _value;
        public TValue Value => IsSuccess ? _value : throw new InvalidOperationException("Cannot access the value of a failed result.");

        #region Constructors
        // Ok - Success
        private Result(TValue value)
        {
            _value = value;
        }
        // Fail With One Error
        private Result(Error error) : base(error)
        {
            _value = default!;
        }
        // Fail With Multiple Errors
        private Result(List<Error> errors) : base(errors)
        {
            _value = default!;
        }

        #endregion

        #region Factory Methods
        // Static Factory Methods
        // Ok - Success
        public static Result<TValue> Ok(TValue value) => new Result<TValue>(value);
        // Fail With One Error
        public static new Result<TValue> Fail(Error error) => new Result<TValue>(error);
        // Fail With Multiple Errors
        public static new Result<TValue> Fail(List<Error> errors) => new Result<TValue>(errors);

        #endregion

        #region Casting Operator
        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator TValue(Result<TValue> result) => result.Value;
        public static implicit operator Result<TValue>(Error error) => Fail(error);
        public static implicit operator Result<TValue>(List<Error> error) => Fail(error);


        #endregion

    }
}
