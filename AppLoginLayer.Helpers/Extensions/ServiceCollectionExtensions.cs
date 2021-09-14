using AppLoginLayer.Helpers.Models.Configurations;
using AppLoginLayer.Helpers.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppLoginLayer.Helpers.Extensions {
	public static class ServiceCollectionExtensions {
		public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, AuthenticationTokenOptions authenticationTokenOptions) {
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options => {
					options.RequireHttpsMetadata = false;
					options.TokenValidationParameters = new TokenValidationParameters {
						ValidateIssuer = true,
						ValidIssuer = authenticationTokenOptions.Issuer,
						ValidateAudience = true,
						ValidAudience = authenticationTokenOptions.Audience,
						IssuerSigningKey = authenticationTokenOptions.GetSymmetricSecurityKey(),
						ValidateIssuerSigningKey = true,
						ClockSkew = TimeSpan.Zero
					};
					options.Events = new JwtBearerEvents {
						OnAuthenticationFailed = context => {
							var authorizationKey = context.Request.Headers.Keys.FirstOrDefault(x =>
								string.Equals(x, SecurityTokenService.AuthorizationKey, StringComparison.CurrentCultureIgnoreCase));
							if (authorizationKey == null) return Task.CompletedTask;

							var accessToken = context.Request.Headers[authorizationKey];
							var handler = new JwtSecurityTokenHandler();
							var jwtSecurityToken = handler.ReadToken(accessToken.ToString().Substring(7)) as JwtSecurityToken;
							if ((DateTime.UtcNow - jwtSecurityToken.ValidTo).Minutes > authenticationTokenOptions.LifetimeMinutes) return Task.CompletedTask;

							var tokenService = context.HttpContext.RequestServices.GetRequiredService<SecurityTokenService>();
							var refreshedSecurityToken = tokenService.GenerateJwtToken(jwtSecurityToken.Claims);
							context.Response.Headers[authorizationKey] = $"bearer {handler.WriteToken(refreshedSecurityToken)}";
							context.Response.Headers[SecurityTokenService.AuthorizationExpiresKey] = refreshedSecurityToken.ValidTo.ToString();

							return Task.CompletedTask;
						}
					};
				});

			return services;
		}

		public static IServiceCollection AddSwagger(this IServiceCollection services, string name, string docPath) {
			services.AddSwaggerGen(c => {
				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
					In = ParameterLocation.Header,
					Description = "Please insert JWT with Bearer into field",
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement {
					{
						new OpenApiSecurityScheme {
							Reference = new OpenApiReference {
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[] { }
					}
				});
				c.SwaggerDoc("v1",
					new OpenApiInfo {
						Title = name,
						Version = "v1"
					}
				);
				var filePath = Path.Combine(System.AppContext.BaseDirectory, docPath);
				c.IncludeXmlComments(filePath);
			});
			return services;
		}
	}
}
