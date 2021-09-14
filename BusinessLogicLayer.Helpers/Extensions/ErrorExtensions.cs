using BusinessLogicLayer.Helpers.Models.Errors;

namespace BusinessLogicLayer.Helpers.Extensions {
	public static class ErrorExtensions {
		public static BaseError SetInnerError(this BaseError error, BaseError innerError) {
			error.InnerError = innerError;
			return error;
		}
	}
}
