using CareHomeTaskManager.Core.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Api
{
    public static class AuthenticationConfig
    {
        public static string GenerateJWT(List<KeyValuePair<string, string>> claimListAsKeyValuePair
            , int tokenValidMinutes, string issuer, string owner)
        {
            return AuthenticationProvider.ProvideToken(claimListAsKeyValuePair, tokenValidMinutes, issuer, owner);
        }
        public static void ConfigureJwtAuthentication(this IServiceCollection services,string secret,
            string validIssuer, string validAudience, bool requireSignedTokens = true,
            bool validateLifeTime = true, bool validateAudience = true, bool validateIssuerSigningkey = true, int clockSkew = 10
            )
        {

            services.AddAuthentication(options => { options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; })
                  .AddJwtBearer(options =>
                  {
                      options.TokenValidationParameters = AuthenticationProvider.GetTokenValidationParameters(
                         secret, validIssuer, validAudience, requireSignedTokens, validateLifeTime, validateAudience,
                          validateIssuerSigningkey, clockSkew);
#if PROD || UAT
                    options.IncludeErrorDetails = false;
#elif DEBUG
                      options.RequireHttpsMetadata = false;
#endif
                  });
        }
    }
}
