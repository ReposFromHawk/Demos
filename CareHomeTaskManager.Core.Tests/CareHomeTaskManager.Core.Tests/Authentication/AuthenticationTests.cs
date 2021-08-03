using CareHomeTaskManager.Core.Authentication;
using Microsoft.IdentityModel.Tokens;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareHomeTaskManager.Core.Tests.Authentication
{
    public class AuthenticationTests
    {

        public AuthenticationTests()
        {
            _claims= new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("UserName","Test User"),
                new KeyValuePair<string, string>("Role","Manager"),
                new KeyValuePair<string, string>("Role","User"),
            };
            _tokenExpirationSetting = 10;
            _issuer = "https://localhost:44393/";
            _owner = "https://localhost:44393/";
        }
        private List<KeyValuePair<string, string>> _claims;
        private int _tokenExpirationSetting;
        private string _issuer;
        private string _owner;
        
        #region retired
        //[Test]
        //public void JwtSecurityHandlerShouldReturnToken()
        //{
        //    AuthenticationProvider provider = new AuthenticationProvider();
        //    string JwtToken = provider.ProvideToken(_claims);
        //    Assert.That(JwtToken, Is.Not.Null);
        //}
        #endregion
        [Test]
        public void JwtSecurityHandlerShouldAcceptUserClaimsSettingParametersAndShouldReturnToken()
        {
            string JwtToken = AuthenticationProvider.ProvideToken(_claims,_tokenExpirationSetting,_issuer,_owner);
            Assert.That(JwtToken, Is.Not.Null);
        }
        #region Retired Due To Static Conversion
        //[Test]
        //public void AutheticationProviderShouldReturnSecureKey()
        //{
        //    var secureKey = AuthenticationProvider.GetSecureKey();
        //    Assert.That(secureKey.GetType(), Is.EqualTo(typeof(SymmetricSecurityKey)));
        //}
        //[Test]
        //public void AuthenticationProviderShouldReturnSigningCredentials()
        //{
        //    AuthenticationProvider provider = new AuthenticationProvider();
        //    var secureKey = provider.SigningCredentials();
        //    Assert.That(secureKey.GetType(), Is.EqualTo(typeof(SigningCredentials)));
        //}
        //[Test]
        //public void AuthenticationProviderShouldReturnTokenValidationParameters()
        //{
        //    AuthenticationProvider provider = new AuthenticationProvider();
        //    var tokenValidationParameters = provider.GetTokenValidationParameters();
        //    Assert.That(tokenValidationParameters.GetType(), Is.EqualTo(typeof(TokenValidationParameters)));
        //}
        #endregion
        
    }
}
