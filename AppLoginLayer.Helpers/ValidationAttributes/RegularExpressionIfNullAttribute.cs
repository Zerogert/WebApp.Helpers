using System;
using System.ComponentModel.DataAnnotations;

namespace AppLoginLayer.Helpers.ValidationAttributes {
	public class RegularExpressionIfNullAttribute : CustomRegularExpressionAttribute {
		public string PropertyName { get; set; }
		public RegularExpressionIfNullAttribute(string propertyName, string pattern) : base(pattern) {
			PropertyName = propertyName;
		}

		protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
			var isValid = base.IsValid(value, validationContext);
			object instance = validationContext.ObjectInstance;
			Type type = instance.GetType();

			var valueProperty = type.GetProperty(PropertyName).GetValue(instance);
			return valueProperty == null ? isValid : ValidationResult.Success;
		}
	}
}
