using AppLoginLayer.Helpers.Extensions;
using AppLoginLayer.Helpers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace AppLoginLayer.Helpers.Interfaces {
	public static class ControllerExtensions {
		public static ICollection<ErrorResponse> GetModelErrors(this ControllerBase controller) {
			return controller.ModelState
				.Where(v => v.Value.ValidationState == ModelValidationState.Invalid)
				.SelectMany(f => f.Value.Errors
					.Select(er => new ErrorResponse {
						Message = er.GetErrorMessage(),
						Type = er.GetErrorType(),
						AdditionalData = new Dictionary<string, string>() { { "Source", f.Key } }
					}))
				.ToList();
		}
	}
}
