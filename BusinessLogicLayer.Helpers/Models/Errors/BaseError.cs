using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Helpers.Models.Errors {
	public class BaseError : Exception {
		public string Type { get; private set; }
		public Dictionary<string, string> AdditionalData { get; set; }
		public BaseError InnerError { get; set; }

		public BaseError(string message, BaseError innerError = null, Exception exception = null):base(message, exception) {
			InnerError = innerError;
			Type = GetType().Name.ToString().Replace("Error", "").Replace("Exception", "");
		}

		public BaseError(string type, string message, BaseError innerError = null, Exception exception = null) : this(message, innerError, exception) {
			Type = type;
		}
	}
}
