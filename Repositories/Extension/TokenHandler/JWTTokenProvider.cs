using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Repository.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Repository.CustomFunctions.TokenHandler
{
    public sealed class JWTTokenProvider(IConfiguration config)
    {
        /*
         * tl;dr on how jwt token works
         * each token is a long string of these json properties groups (separated by a dot .) encrypted in Base64:
         *  + The header (encoded base64, can be decoded into plain text):
         *      - jwt signing algorithm: hs256 or rs256 for example
         *      - token type: usually jwt maybe
         *  + The payload (encoded base64, can be decoded into plain text):
         *      - token claims: sub (uid), email, custom claims like role and username for example
         *      - other data to send with the token
         *  + The signature (the more complex part):
         *      - created with the defined signing algorithm
         *      - using the datas and the secret key to create a unique token
         *      - the secret key can be user-defined, or an arbitrary/encrypted/encoded/generated random string
         * how the token is actually verified server-side:
         *  + server generate a test signature using the data received in the token
         *  + if the test signature matches the received signature, it verified that the header and payload has not been modified
         *  + plus if an attacker were to tamper the data, they do not have knowledge of the secret key to sign the token
         *  + essentially ensuring a nonreplicable unique token
         */

        //One way of generating a token
        /// <summary>
        /// .Include() Role in the User object sent to this
        /// </summary>
        public string CreateToken(User user)
        {
            if(user == null)
            {
                throw new ArgumentException("User is null");
            }
            //get secret authkey in the config
            string secretKey = config["JWT:Secret"];
            //encode the secret key
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            //define the signing algorithm and generation of the signature
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //define the payload
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.Name),
                        new Claim("RoleId", user.RoleId.ToString())
                        //new Claim("email_verified",)
                    ]),
                Expires = DateTime.UtcNow.AddMinutes(config.GetValue<int>("JWT:ExpirationTime")),
                SigningCredentials = credentials,
                Issuer = config["JWT:Issuer"],
                Audience = config["JWT:Audience"]
            };
            var handler = new JsonWebTokenHandler();

            string token = handler.CreateToken(tokenDescriptor);

            return token;
        }
        
        //Implement Refresh TOken here




        //this is another way (older way and less secure (only take username) i think)
        public string GenerateVerificationToken(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));
            }

            string secretKey = config["Jwt:secret"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(
                    [
                        new Claim(ClaimTypes.NameIdentifier, username),
                    ]),
                Issuer = config["Jwt:Issuer"],
                Audience = config["Jwt:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(config.GetValue<int>("Jwt:ExpirationTime")),
                SigningCredentials = credentials
            };

            return new JsonWebTokenHandler().CreateToken(token);
        }
    }
}
