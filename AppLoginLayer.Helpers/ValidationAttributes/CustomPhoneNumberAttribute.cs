using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AppLoginLayer.Helpers.ValidationAttributes {
	public class CustomPhoneNumberAttribute : ValidationAttribute {
		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			var phoneResult = true;
			if (value == null) return ValidationResult.Success;

			var phone = (string)value;
			if (phone.Length < 11) phoneResult = false;
			if (phone.Length > 12) phoneResult = false;
			if (!phone.All(d => char.IsDigit(d) || d == '+')) phoneResult = false;

			if (phoneResult) return ValidationResult.Success;
			var errorMessage = ErrorMessageString ?? new PhoneAttribute().ErrorMessage;
			var result = new ValidationResult("PhoneNumberError*|*" + string.Format(errorMessage, validationContext.DisplayName));
			return result;
		}
	}
}
