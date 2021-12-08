using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        string awsAccessKeyId = CredentialsV2.AccessKey; //AWSCredentials.awsAccessKeyId;//
        string awsSecretAccessKey = CredentialsV2.SecretKey; //AWSCredentials.awsSecretAccessKey; //
        string awsSessionToken = CredentialsV2.SessionToken; // AWSCredentials.awsSessionToken; //
        RegionEndpoint region = AWSCredentials.region;
        string bucketName = AWSCredentials.bucketName;
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string UUID = "";
            using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    UUID = uploadRequest.Key;
                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
            return Created(UUID, file);
        }
        [HttpGet]
        public async Task<FileResult> Download(string key)
        {
            byte[] msByteArray;
            string contentType;
            using (var client = new AmazonS3Client(CredentialsV2.AccessKey, CredentialsV2.SecretKey, CredentialsV2.SessionToken, region))
            {
                MemoryStream ms = new MemoryStream();
                using (GetObjectResponse response = await client.GetObjectAsync(bucketName, key))
                {
                    response.ResponseStream.CopyTo(ms);
                    contentType = response.Headers.ContentType.ToString();
                }
                msByteArray = ms.ToArray();
            }
            return File(msByteArray, contentType, key);
        }
    }
}