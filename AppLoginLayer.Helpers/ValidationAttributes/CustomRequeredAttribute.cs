using System.ComponentModel.DataAnnotations;

namespace AppLoginLayer.Helpers.ValidationAttributes {
	public class CustomRequiredAttribute : RequiredAttribute {
		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			var result = base.IsValid(value, validationContext);
			if (result != null) result.ErrorMessage = "FieldRequiredError*|*" + result.ErrorMessage;
			return result;
		}
	}
}
