using System;
using System.Collections.Generic;
using System.Text;

namespace AppLoginLayer.Helpers.Models {
	public class ErrorResponse {
		public string Type { get; set; }
		public string Message { get; set; }
		public Dictionary<string, string> AdditionalData { get; set; }
	}
}
