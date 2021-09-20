using BusinessLogicLayer.Helpers.Models;
using BusinessLogicLayer.Helpers.Models.Errors;
using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Helpers.Extensions {
	public static class ServiceResultExtensions {
		public static ServiceResult<TOut> ConvertResult<TIn, TOut>(this ServiceResult<TIn> result, Func<TIn, TOut> resultFormatter, BaseError error) where TIn : class where TOut : class {
			return result.Succeeded ? ServiceResult<TOut>.Ok(resultFormatter.Invoke(result.Data)) : ServiceResult<TOut>.Fail(error.SetInnerError(result.Error));
		}

		public static ServiceResult<TOut> ConvertResult<TIn, TOut>(this ServiceResult<TIn> result, Func<TIn, TOut> resultFormatter) where TIn : class where TOut : class {
			return result.Succeeded ? ServiceResult<TOut>.Ok(resultFormatter.Invoke(result.Data)) : ServiceResult<TOut>.Fail(result.Error);
		}

		public static async Task<ServiceResult<TIn>> RunActionAsync<TIn>(this ServiceResult<TIn> result, Func<TIn, Task> resultRun) where TIn : class {
			if (!result.Succeeded) return result;
			await resultRun.Invoke(result.Data);
			return result;
		}

		public static async Task<ServiceResult<TOut>> ContinueResultAsync<TIn, TOut>(this ServiceResult<TIn> result, Func<TIn, Task<ServiceResult<TOut>>> nextResultFunc, Func<TIn, TOut, TOut> resultFormatter) where TIn : class where TOut : class {
			if (!result.Succeeded) return ServiceResult<TOut>.Fail(result.Error);
			var nextResult = await nextResultFunc.Invoke(result.Data);
			if (!nextResult.Succeeded) return ServiceResult<TOut>.Fail(nextResult.Error);

			return ServiceResult<TOut>.Ok(resultFormatter.Invoke(result.Data, nextResult.Data));
		}

		public static ServiceResult<TOut> UnionResult<TIn, TOut>(this ServiceResult<TIn> result, ServiceResult<TOut> nextResult, Func<TIn, TOut, TOut> resultFormatter) where TIn : class where TOut : class {
			if (!result.Succeeded) return ServiceResult<TOut>.Fail(result.Error);
			if (!nextResult.Succeeded) return ServiceResult<TOut>.Fail(nextResult.Error);

			return ServiceResult<TOut>.Ok(resultFormatter.Invoke(result.Data, nextResult.Data));
		}
	}
}
