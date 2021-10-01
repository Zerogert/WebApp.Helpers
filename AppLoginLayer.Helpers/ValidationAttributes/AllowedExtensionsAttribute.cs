using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;

namespace AppLoginLayer.Helpers.ValidationAttributes {

	/// <summary>
	/// Specifies allowed extensions for file in property.
	/// <remarks>
	/// This attribute may be used only for IFormFile
	/// </remarks>
	/// </summary>
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public class AllowedExtensionsAttribute : ValidationAttribute {

		private readonly string[] _allowedExtensions;

		/// <summary>
		/// Initializes a new instance of the <see cref="AllowedExtensionsAttribute"/> class.
		/// </summary>
		/// <param name="allowedExtensions"></param>
		public AllowedExtensionsAttribute(string allowedExtensions) {
			_allowedExtensions = allowedExtensions.Split('|');
		}

		/// <summary>
		/// Determines whether a specified object is valid. (Overrides <see cref = "ValidationAttribute.IsValid(object)" />)
		/// </summary>
		/// <remarks>
		/// This method returns <c>true</c> if the <paramref name = "value" /> is null.  
		/// It is assumed the <see cref = "RequiredAttribute" /> is used if the value may not be null.
		/// </remarks>
		/// <param name="value">The object to validate.</param>
		/// <returns><c>true</c> if the value is null or less than or equal to the specified maximum length, otherwise <c>false</c></returns>
		public override bool IsValid(object value) {
			var file = value as IFormFile;

			if (file == null) {
				return true;
			}

			var extension = Path.GetExtension(file.FileName);

			return _allowedExtensions.Contains(extension);
		}

		public override string FormatErrorMessage(string name) {
			return "AllowedExtensionsError*|*" + string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, string.Join('|', _allowedExtensions));
		}
	}
}
