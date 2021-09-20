using AppLoginLayer.Helpers.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppLoginLayer.Helpers.Extensions {
	public static class ControllerBaseExtensions {
		public static ModelValidationResult ValidateModel(this ControllerBase controller) {
			if (controller.ModelState.IsValid) return ModelValidationResult.Ok();
			return ModelValidationResult.Ok();
		}

	}
}
