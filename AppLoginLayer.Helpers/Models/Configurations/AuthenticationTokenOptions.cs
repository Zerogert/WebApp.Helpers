using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AppLoginLayer.Helpers.Models.Configurations {
	public class AuthenticationTokenOptions {
		public string Issuer { get; set; }
		public string Audience { get; set; }
		public string SecretKey { get; set; }
		public int LifetimeMinutes { get; set; }
		public SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
	}
}
