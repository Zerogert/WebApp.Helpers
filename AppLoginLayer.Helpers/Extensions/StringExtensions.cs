using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace AppLoginLayer.Helpers.Extensions {
	public static class StringExtensions {
		public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct, Enum {
			if (string.IsNullOrEmpty(value)) return default;
			return Enum.TryParse(value, true, out TEnum result) ? result : default;
		}
	}
}
