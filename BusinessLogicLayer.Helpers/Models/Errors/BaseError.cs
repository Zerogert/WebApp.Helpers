using System;
using System.Collections.Generic;

namespace BusinessLogicLayer.Helpers.Models.Errors {
	public class BaseError : Exception {
		public string Type { get; private set; }
		public Dictionary<string, string> AdditionalData { get; set; }
		public BaseError InnerError { get; set; }
		public Exception Exception { get; set; }

		public BaseError(string message, BaseError innerError = null, Exception exception = null) {
			InnerError = innerError;
			Exception = exception;
			Type = GetType().Name.Replace("Error", "").Replace("Exception", "");
		}

		public BaseError(string type, string message, BaseError innerError = null, Exception exception = null) : this(message, innerError, exception) {
			Type = type;
		}

	}
}
