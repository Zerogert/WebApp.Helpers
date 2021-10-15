using System.ComponentModel.DataAnnotations;

namespace AppLoginLayer.Helpers.ValidationAttributes {
	public class CustomEmailAttribute : ValidationAttribute {
		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			var emailResult = new EmailAddressAttribute().IsValid(value);
			if (emailResult) return ValidationResult.Success;
			var errorMessage = ErrorMessageString ?? new EmailAddressAttribute().ErrorMessage;
			var result = new ValidationResult("EmailAddressError*|*" + string.Format(errorMessage, validationContext.DisplayName));
			return result;
		}
	}
}
