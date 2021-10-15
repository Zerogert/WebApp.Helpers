using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;

namespace AppLoginLayer.Helpers.Extensions {
	public static class ConfigurationExtensions {
		public static List<CultureInfo> GetSupportingCultures(this IConfiguration configuration) {
			return configuration.GetSection("Localization:SupportedCultures")
				.Get<List<string>>()
				.Select(c => new CultureInfo(c))
				.ToList();
		}

		public static RequestCulture GetDefaultCulture(this IConfiguration configuration) {
			return new RequestCulture(configuration["Localization:DefaultCulture"]);
		}

		public static string[] GetAllowedOrigins(this IConfiguration configuration) {
			var allowedOrigins = configuration.GetSection("CORSOrigins").Get<string[]>();
			return allowedOrigins;
		}
	}
}
