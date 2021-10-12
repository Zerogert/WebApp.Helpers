using System;

namespace BusinessLogicLayer.Helpers.Extensions {
	public static class DateTimeExtensions {
		public static bool IsExpired(this DateTime date, TimeSpan expirationTerm) {
			var dateExpired = date + expirationTerm;
			return dateExpired <= DateTime.UtcNow;
		}
	}
}
