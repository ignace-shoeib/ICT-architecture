using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using System.Threading.Tasks;
#region BRONNEN
// CREATE USERS:     https://aws.amazon.com/blogs/mobile/use-csharp-to-register-and-authenticate-with-amazon-cognito-user-pools/
// AUTHFLOW:         https://aws.amazon.com/blogs/mobile/use-csharp-to-register-and-authenticate-with-amazon-cognito-user-pools/
// AUTHFLOW FIX:     https://stackoverflow.com/questions/48757864/aws-cognito-user-password-auth-initiate-auth-method-not-supported
#endregion
namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        string awsAccessKeyId = AWSCredentials.AccessKey;
        string awsSecretAccessKey = AWSCredentials.SecretKey;
        string awsSessionToken = AWSCredentials.SessionToken;
        RegionEndpoint region = AWSCredentials.region;
        string appClientId = AWSCredentials.AppClientId;
        string poolId = AWSCredentials.PoolId;
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [Route("Signup")]
        [HttpPost]
        public async Task<IActionResult> CreateUser(string email, string password)
        {
            AmazonCognitoIdentityProviderClient client = new AmazonCognitoIdentityProviderClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region);
            var signUpRequest = new SignUpRequest
            {
                ClientId = appClientId,
                Password = password,
                Username = email,
            };
            try
            {
                await client.SignUpAsync(signUpRequest);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
            return Created("", signUpRequest);
        }
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("Signin")]
        [HttpPost]
        public async Task<IActionResult> AuthenticateUser(string email, string password)
        {
            var authReq = new AdminInitiateAuthRequest
            {
                UserPoolId = poolId,
                ClientId = appClientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
            };
            authReq.AuthParameters.Add("USERNAME", email);
            authReq.AuthParameters.Add("PASSWORD", password);
            AmazonCognitoIdentityProviderClient client = new AmazonCognitoIdentityProviderClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region);
            AdminInitiateAuthResponse authRes;
            try
            {
                authRes = await client.AdminInitiateAuthAsync(authReq);
            }
            catch (System.Exception)
            {
                return BadRequest();
            }
            HttpContext.Response.Cookies.Append("id_token", authRes.AuthenticationResult.IdToken, new CookieOptions { HttpOnly = true });
            return Ok(authRes.AuthenticationResult);
        }
    }
}