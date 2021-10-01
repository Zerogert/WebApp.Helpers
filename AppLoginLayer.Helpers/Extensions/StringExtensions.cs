using System;

namespace AppLoginLayer.Helpers.Extensions {
	public static class StringExtensions {
		public static TEnum ToEnum<TEnum>(this string value) where TEnum : struct, Enum {
			if (string.IsNullOrEmpty(value)) return default;
			return Enum.TryParse(value, true, out TEnum result) ? result : default;
		}

		public static bool IsHasErrorType(this string value) {
			return !string.IsNullOrEmpty(value) && value.Contains("*|*");
		}

		public static string GetErrorType(this string value) {
			return string.IsNullOrEmpty(value) || !value.IsHasErrorType() ? string.Empty : value.Substring(0, value.IndexOf("*|*"));
		}

		public static string GetErrorMessage(this string value) {
			if (string.IsNullOrEmpty(value)) return string.Empty;
			if (!value.IsHasErrorType()) return value;
			var indexErrorType = value.IndexOf("*|*") + 3;
			return value.Substring(indexErrorType, value.Length - indexErrorType);
		}
	}
}
