using BusinessLogicLayer.Helpers.Models;
using BusinessLogicLayer.Helpers.Models.Errors;
using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Helpers.Extensions {
	public static class ServiceResultExtensions {
		public static ServiceResult<TOut> ConvertResult<TIn, TOut>(this ServiceResult<TIn> result, Func<TIn, TOut> resultFormatter, BaseError error) where TIn : class where TOut : class {
			return result.Succeeded ? ServiceResult<TOut>.Ok(resultFormatter.Invoke(result.Data)) : ServiceResult<TOut>.Fail(error.SetInnerError(result.Error));
		}
	}
}
