using BusinessLogicLayer.Helpers.Models;
using BusinessLogicLayer.Helpers.Models.Errors;
using System;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Helpers.Extensions {
	public static class ServiceResultExtensions {
		public static ServiceResult<T> ToServiceResult<T>(this T data) where T : class {
			return ServiceResult<T>.Ok(data);
		}

		public static ServiceResult<TOut> ConvertResult<TIn, TOut>(this ServiceResult<TIn> result, Func<TIn, TOut> resultFormatter, BaseError error) where TIn : class where TOut : class {
			return result.Succeeded ? ServiceResult<TOut>.Ok(resultFormatter.Invoke(result.Data)) : ServiceResult<TOut>.Fail(error.SetInnerError(result.Error));
		}

		public static ServiceResult<TOut> ConvertResult<TIn, TOut>(this ServiceResult<TIn> result, Func<TIn, TOut> resultFormatter) where TIn : class where TOut : class {
			return result.Succeeded ? ServiceResult<TOut>.Ok(resultFormatter.Invoke(result.Data)) : ServiceResult<TOut>.Fail(result.Error);
		}

		public static async Task<ServiceResult<TOut>> ConvertResult<TIn, TOut>(this Task<ServiceResult<TIn>> resultTask, Func<TIn, TOut> resultFormatter) where TIn : class where TOut : class {
			var result = await resultTask;
			return ConvertResult(result, resultFormatter);
		}

		public static async Task<ServiceResult<TOut>> ConvertResultAsync<TIn, TOut>(this ServiceResult<TIn> result, Func<TIn, Task<TOut>> resultFormatter) where TIn : class where TOut : class {
			return result.Succeeded ? ServiceResult<TOut>.Ok(await resultFormatter.Invoke(result.Data)) : ServiceResult<TOut>.Fail(result.Error);
		}

		public static async Task<ServiceResult<TOut>> ConvertResultAsync<TIn, TOut>(this Task<ServiceResult<TIn>> resultTask, Func<TIn, Task<TOut>> resultFormatter) where TIn : class where TOut : class {
			var result = await resultTask;
			return await ConvertResultAsync(result, resultFormatter);
		}

		public static ServiceResult<TIn> CheckResult<TIn, TOut>(this ServiceResult<TIn> result, ServiceResult<TOut> checkingResult) where TIn : class where TOut : class {
			if (!result.Succeeded) return result;
			if (!checkingResult.Succeeded) return ServiceResult<TIn>.Fail(checkingResult.Error);
			return result;
		}

		public static async Task<ServiceResult<TIn>> RunServiceAsync<TIn>(this ServiceResult<TIn> result, Func<TIn, Task<ServiceResult>> service, Action<TIn> action = null, Func<TIn, bool> condition = null) where TIn : class {
			if (!result.Succeeded) return result;
			if (condition != null && !condition.Invoke(result.Data)) return result;
			var runServiceResult = await service.Invoke(result.Data);
			if (runServiceResult.Succeeded && action != null) action.Invoke(result.Data);
			if (!runServiceResult.Succeeded) return ServiceResult<TIn>.Fail(runServiceResult.Error);
			return result;
		}

		public static async Task<ServiceResult<TIn>> RunServiceAsync<TIn, TOut>(this Task<ServiceResult<TIn>> resultTask, Func<TIn, Task<ServiceResult<TOut>>> service, Action<TIn, TOut> action = null, Func<TIn, bool> condition = null) where TIn : class where TOut : class {
			var result = await resultTask;
			return await RunServiceAsync(result, service, action, condition);
		}

		public static async Task<ServiceResult<TIn>> RunServiceAsync<TIn, TOut>(this ServiceResult<TIn> result, Func<TIn, Task<ServiceResult<TOut>>> service, Action<TIn, TOut> action = null, Func<TIn, bool> condition = null) where TIn : class where TOut : class {
			if (!result.Succeeded) return result;
			if (condition != null && !condition.Invoke(result.Data)) return result;
			var runServiceResult = await service.Invoke(result.Data);
			if (runServiceResult.Succeeded && action != null) action.Invoke(result.Data, runServiceResult.Data);
			if (!runServiceResult.Succeeded) return ServiceResult<TIn>.Fail(runServiceResult.Error);
			return result;
		}

		public static async Task<ServiceResult<TIn>> RunServiceAsync<TIn>(this Task<ServiceResult<TIn>> resultTask, Func<TIn, Task<ServiceResult>> service, Action<TIn> action = null, Func<TIn, bool> condition = null) where TIn : class {
			var result = await resultTask;
			return await RunServiceAsync(result, service, action, condition);
		}

		public static async Task<ServiceResult<TIn>> RunActionAsync<TIn>(this ServiceResult<TIn> result, Func<TIn, Task> resultRun) where TIn : class {
			if (!result.Succeeded) return result;
			await resultRun.Invoke(result.Data);
			return result;
		}

		public static async Task<ServiceResult<TIn>> RunActionAsync<TIn>(this Task<ServiceResult<TIn>> resultTask, Func<TIn, Task> resultRun) where TIn : class {
			var result = await resultTask;
			return await RunActionAsync(result, resultRun);
		}

		public static ServiceResult<TIn> RunAction<TIn>(this ServiceResult<TIn> result, Action<TIn> resultRun) where TIn : class {
			if (!result.Succeeded) return result;
			resultRun.Invoke(result.Data);
			return result;
		}

		public static async Task<ServiceResult<TIn>> ContinueResultAsync<TIn>(this Task<ServiceResult<TIn>> resultTask, Func<TIn, Task<ServiceResult<TIn>>> nextResultFunc, Action<TIn, TIn> action = null) where TIn : class {
			var result = await resultTask;
			return await result.ContinueResultAsync(nextResultFunc, action);
		}

		public static async Task<ServiceResult<TIn>> ContinueResultAsync<TIn>(this ServiceResult<TIn> result, Func<TIn, Task<ServiceResult<TIn>>> nextResultFunc, Action<TIn, TIn> action = null) where TIn : class {
			if (!result.Succeeded) return ServiceResult<TIn>.Fail(result.Error);
			var nextResult = await nextResultFunc.Invoke(result.Data);
			if (!nextResult.Succeeded) return ServiceResult<TIn>.Fail(nextResult.Error);
			if (action != null) action.Invoke(result.Data, nextResult.Data);
			return nextResult;
		}

		public static ServiceResult<TIn> ContinueResult<TIn>(this ServiceResult<TIn> result, Func<TIn, ServiceResult<TIn>> nextResultFunc) where TIn : class {
			if (!result.Succeeded) return ServiceResult<TIn>.Fail(result.Error);
			var nextResult = nextResultFunc.Invoke(result.Data);
			if (!nextResult.Succeeded) return ServiceResult<TIn>.Fail(nextResult.Error);

			return nextResult;
		}

		public static async Task<ServiceResult<TOut>> NextResultAsync<TIn, TOut>(this ServiceResult<TIn> result, Func<TIn, Task<ServiceResult<TOut>>> nextResultFunc) where TIn : class where TOut : class {
			if (!result.Succeeded) return ServiceResult<TOut>.Fail(result.Error);
			var nextResult = await nextResultFunc.Invoke(result.Data);

			return nextResult;
		}
	}
}
