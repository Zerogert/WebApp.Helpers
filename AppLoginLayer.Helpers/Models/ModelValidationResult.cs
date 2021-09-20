using BusinessLogicLayer.Helpers.Models.Errors;
using System.Collections.Generic;

namespace AppLoginLayer.Helpers.Models {
	public class ModelValidationResult {
		public bool Succeeded { get; set; }
		public ICollection<BaseError> Errors { get; set; }

		public static ModelValidationResult Ok() {
			return new ModelValidationResult() { Succeeded = true };
		}

		public static ModelValidationResult Fail(ICollection<BaseError> errors) {
			return new ModelValidationResult() { Errors = errors, Succeeded = false };
		}
	}
}
