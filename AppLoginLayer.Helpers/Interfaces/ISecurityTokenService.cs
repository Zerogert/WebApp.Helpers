using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppLoginLayer.Helpers.Interfaces {
	public interface ISecurityTokenService {
		JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims);
	}
}
