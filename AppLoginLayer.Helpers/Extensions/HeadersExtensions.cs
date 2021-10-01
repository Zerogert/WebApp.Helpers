using Microsoft.AspNetCore.Http;

namespace AppLoginLayer.Helpers.Extensions {
	public static class HeadersExtensions {
		public static IHeaderDictionary AddOrUpdate(this IHeaderDictionary headers, string key, string value) {
			if (headers.ContainsKey(key)) {
				headers[key] = value;
			} else {
				headers.Add(key, value);
			}

			return headers;
		}
	}
}
