using System;
using System.ComponentModel.DataAnnotations;

namespace AppLoginLayer.Helpers.ValidationAttributes {
	public class CustomRangeAttribute : RangeAttribute {
		public CustomRangeAttribute(double minimum, double maximum) : base(minimum, maximum) {
		}

		public CustomRangeAttribute(int minimum, int maximum) : base(minimum, maximum) {
		}

		public CustomRangeAttribute(Type type, string minimum, string maximum) : base(type, minimum, maximum) {
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			var result = base.IsValid(value, validationContext);
			if (result != null) result.ErrorMessage = "RangeError*|*" + result.ErrorMessage;
			return result;
		}
	}
}
