using System.ComponentModel.DataAnnotations;

namespace AppLoginLayer.Helpers.ValidationAttributes {
	public class CustomStringLengthAttribute : StringLengthAttribute {
		public CustomStringLengthAttribute(int maximumLength) : base(maximumLength) {
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			var result = base.IsValid(value, validationContext);
			if (result != null) result.ErrorMessage = "StringLengthError*|*" + result.ErrorMessage;
			return result;
		}
	}
}
