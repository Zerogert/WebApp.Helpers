using System.Collections.Generic;

namespace BusinessLogicLayer.Helpers.Extensions {
	public static class ParametersExtensions {
		public static ICollection<KeyValuePair<string, string>> AddParameter(this ICollection<KeyValuePair<string, string>> parameters, string key, string value) {
			parameters.Add(new KeyValuePair<string, string>(key, value));
			return parameters;
		}
	}
}
