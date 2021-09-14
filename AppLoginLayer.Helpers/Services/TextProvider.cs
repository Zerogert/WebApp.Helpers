using BusinessLogicLayer.Helpers.Interfaces;
using Microsoft.Extensions.Localization;

namespace AppLoginLayer.Helpers.Services {
	public class TextProvider<TRes> : ITextProvider {
		private readonly IStringLocalizer<TRes> _localizer;
		public TextProvider(IStringLocalizer<TRes> localizer) {
			_localizer = localizer;
		}

		public string this[string index] => _localizer[index];
	}
}
