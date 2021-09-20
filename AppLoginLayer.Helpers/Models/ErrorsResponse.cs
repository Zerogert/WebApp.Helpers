using System.Collections.Generic;

namespace AppLoginLayer.Helpers.Models {
	public class ErrorsResponse {
		public int ErrorTotal => Errors?.Count ?? 0;
		public string Message { get; set; }
		public ICollection<ErrorResponse> Errors { get; set; }
	}
}
