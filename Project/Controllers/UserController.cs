﻿using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
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
        string awsAccessKeyId = CredentialsV2.AccessKey; //AWSCredentials.awsAccessKeyId;//
        string awsSecretAccessKey = CredentialsV2.SecretKey; //AWSCredentials.awsSecretAccessKey; //
        string awsSessionToken = CredentialsV2.SessionToken; // AWSCredentials.awsSessionToken; //
        RegionEndpoint region = AWSCredentials.region;
        string appClientId = AWSCredentials.appClientId;
        string poolId = AWSCredentials.poolId;
        [HttpPost]
        public IActionResult CreateUser(string email, string password)
        {
            AmazonCognitoIdentityProviderClient client = new AmazonCognitoIdentityProviderClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region);
            var signUpRequest = new SignUpRequest
            {
                ClientId = appClientId,
                Password = password,
                Username = email,
            };
            client.SignUpAsync(signUpRequest);
            return Created("", signUpRequest);
        }
        [HttpGet]
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
            var authRes = await client.AdminInitiateAuthAsync(authReq);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authRes.AuthenticationResult.AccessToken); ;
            return Ok(authRes);
        }
    }
}