using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Core.Authentication
{
    public static class AuthenticationProvider
    {
        private static string _secret = "D9B5F58F0B38198293971865A14074F59EBA3E82595BECBE86AE51F1D9F1F65E";

        public static string ProvideToken(List<KeyValuePair<string,string>> claimListAsKeyValuePair
            ,int tokenValidMinutes,string issuer, string owner)
        {
            JwtSecurityToken token;
            var claimList = new List<Claim>();
            foreach (var item in claimListAsKeyValuePair)
            {
                claimList.Add(new Claim(item.Key, item.Value));
            }
            var claims = claimList.ToArray();
            token = new JwtSecurityToken(
                issuer,owner,claims,DateTime.UtcNow,
                expires:DateTime.Now.AddMinutes(tokenValidMinutes),
                signingCredentials: SigningCredentials()
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static SymmetricSecurityKey GetSecureKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        }

        private static SigningCredentials SigningCredentials()
        {
            return new SigningCredentials(GetSecureKey(), SecurityAlgorithms.HmacSha256);
        }

        public static TokenValidationParameters GetTokenValidationParameters(
            string secret,string validIssuer,string validAudience,bool requireSignedTokens=true,
            bool validateLifeTime=true, bool validateAudience = true, bool validateIssuerSigningkey=true, int clockSkew = 10
            )
        {
            _secret = secret;
            return new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = validateIssuerSigningkey,
                ValidIssuer = validIssuer,
                ValidateLifetime = validateLifeTime,
                ValidAudience = validAudience,
                ValidateAudience = validateAudience,
                RequireSignedTokens = requireSignedTokens,
                IssuerSigningKey = SigningCredentials().Key,
                ClockSkew = TimeSpan.FromMinutes(clockSkew)
            };
        }
        

    }
}
