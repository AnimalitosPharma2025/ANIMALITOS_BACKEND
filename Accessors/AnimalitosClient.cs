using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;

namespace ANIMALITOS_PHARMA_API.Accessors
{
    public partial class AnimalitosClient : _BaseAccessor
    {
        public void Login(string jwtToken, bool databaseRetry = false)
        {
            // Extract the token from the header.
            if (jwtToken.Contains("Bearer"))
            {
                // Remove the "Bearer" prefix.
                jwtToken = AuthenticationHeaderValue.Parse(jwtToken).Parameter;
            }

            // Validate the token.
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            Connect(databaseRetry);
        }
    }
}
