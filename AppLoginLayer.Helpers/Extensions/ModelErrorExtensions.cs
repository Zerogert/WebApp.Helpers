using AppLoginLayer.Helpers.Extensions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace AppLoginLayer.Helpers.Extensions {
	public static class ModelErrorExtensions {
		public static string GetErrorType(this ModelError value) {
			return !value.ErrorMessage.IsHasErrorType() ? string.Empty : value.ErrorMessage.GetErrorType();
		}

		public static string GetErrorMessage(this ModelError value) {
			return string.IsNullOrEmpty(value.ErrorMessage) ? string.Empty : value.ErrorMessage.GetErrorMessage();
		}
	}
}
