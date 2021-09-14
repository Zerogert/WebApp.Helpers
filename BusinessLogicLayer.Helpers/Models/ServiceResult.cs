using BusinessLogicLayer.Helpers.Models.Errors;

namespace BusinessLogicLayer.Helpers.Models {
	public class ServiceResult {
		public bool Succeeded { get; set; }
		public BaseError Error { get; set; }

		public static ServiceResult Ok() => new ServiceResult { Succeeded = true, Error = null };
		public static ServiceResult Fail(BaseError error) => new ServiceResult { Succeeded = false, Error = error };
	}

	public class ServiceResult<TOut> : ServiceResult where TOut : class {
		public TOut Data { get; private set; }

		public static ServiceResult<TOut> Ok(TOut data) => new ServiceResult<TOut> { Succeeded = true, Data = data };
		public static new ServiceResult<TOut> Fail(BaseError error) => new ServiceResult<TOut> { Succeeded = false, Error = error };

		public static implicit operator ServiceResult<object>(ServiceResult<TOut> s) => new ServiceResult<object>() {
			Data = s.Data,
			Error = s.Error,
			Succeeded = s.Succeeded
		};
	}
}
