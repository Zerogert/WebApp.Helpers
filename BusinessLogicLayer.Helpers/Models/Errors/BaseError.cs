using System;

namespace BusinessLogicLayer.Helpers.Models.Errors {
	public class BaseError : Exception {
		public BaseError InnerError { get; set; }
		public Exception Exception { get; set; }
	}
}
