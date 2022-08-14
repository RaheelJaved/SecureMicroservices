using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;

namespace Movies.Client
{
    public static class Extensions
    {

        public static IServiceCollection SetupAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.Authority = "https://localhost:5005";

                options.ClientId = "movies_mvc_client";
                options.ClientSecret = "secret";
                options.ResponseType = "code";

                options.Scope.Add("openid");
                options.Scope.Add("profile");
                //options.Scope.Add("address");
                //options.Scope.Add("email");
                //options.Scope.Add("roles");

                //options.ClaimActions.DeleteClaim("sid");
                //options.ClaimActions.DeleteClaim("idp");
                //options.ClaimActions.DeleteClaim("s_hash");
                //options.ClaimActions.DeleteClaim("auth_time");
                //options.ClaimActions.MapUniqueJsonKey("role", "role");

                //options.Scope.Add("movieAPI");

                options.SaveTokens = true;
                options.GetClaimsFromUserInfoEndpoint = true;

                //options.TokenValidationParameters = new TokenValidationParameters
                //{
                //    NameClaimType = JwtClaimTypes.GivenName,
                //    RoleClaimType = JwtClaimTypes.Role
                //};
            });
            return services;
        }
    }
}
