using AppLoginLayer.Helpers.Interfaces;
using AppLoginLayer.Helpers.Models.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AppLoginLayer.Helpers.Services {
	public class SecurityTokenService : ISecurityTokenService {
		private readonly AuthenticationTokenOptions _tokenOptions;

		public const string AuthorizationKey = "Authorization";
		public const string AuthorizationExpiresKey = "Authorization_expires";

		public SecurityTokenService(AuthenticationTokenOptions tokenOptions) {
			_tokenOptions = tokenOptions;
		}

		public JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims) {
			return GenerateJwtToken(claims, _tokenOptions.LifetimeMinutes);
		}

		public JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> claims, int lifeTimeMinutes) {
			ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme, ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
			var now = DateTime.UtcNow;
			var expires = now.Add(TimeSpan.FromMinutes(lifeTimeMinutes));
			return new JwtSecurityToken(_tokenOptions.Issuer, _tokenOptions.Audience,
				notBefore: now,
				claims: claimsIdentity.Claims,
				expires: expires,
				signingCredentials: new SigningCredentials(_tokenOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
		}
	}
}
