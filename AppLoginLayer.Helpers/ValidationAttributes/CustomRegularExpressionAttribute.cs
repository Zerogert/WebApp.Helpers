using System.ComponentModel.DataAnnotations;

namespace AppLoginLayer.Helpers.ValidationAttributes {
	public class CustomRegularExpressionAttribute : RegularExpressionAttribute {
		public CustomRegularExpressionAttribute(string pattern) : base(pattern) {
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			var result = base.IsValid(value, validationContext);
			if (result != null) result.ErrorMessage = "StringFormatError*|*" + result.ErrorMessage;
			return result;
		}
	}
}
