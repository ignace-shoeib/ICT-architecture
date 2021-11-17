using Amazon;
using Amazon.CognitoIdentity;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        string awsAccessKeyId = AWSCredentials.awsAccessKeyId;
        string awsSecretAccessKey = AWSCredentials.awsSecretAccessKey;
        string awsSessionToken = AWSCredentials.awsSessionToken;
        RegionEndpoint region = AWSCredentials.region;
        string appClientID = AWSCredentials.appClientID;
        [HttpPost]
        public IActionResult CreateUser(string email, string password)
        {
            AmazonCognitoIdentityClient client = new AmazonCognitoIdentityClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region);
            var signUpRequest = new SignUpRequest
            {
                ClientId = appClientID,
                Password = password,
                Username = email,
            };
            return Created("", "");
        }
    }
}