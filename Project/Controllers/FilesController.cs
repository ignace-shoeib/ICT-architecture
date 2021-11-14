using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.S3;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3.Transfer;
using System.IO;
using Amazon.S3.Model;
namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        string awsAccessKeyId = "ASIA5IU4YWEJ5ZYBZDFT";
        string awsSecretAccessKey = "OcJLhyS9UyKmSv+c01VG2tE/jjzyRHlIksXeyftc";
        string awsSessionToken = "FwoGZXIvYXdzEL3//////////wEaDCjZfP3ZY4kheTffVSLMAZjsEpm/ZE2nBQ8bUub3EwQmPkdvxAi7GEXVEYwssSoYwG+YSDBvn9MpB5Lj68nCKCCA1/OaE7TBmZC6BhseRAViBg0Klphfn6gijIkUoQAujrmGEPJ27/iZdw3kPdmGB0BO6rfncr/9OgRJAhX5lwpKNzY5Pr0G2tYy2lrDxPRdPJwyeRLjK0fP2k+m5cREDfWuvfWDVzCdY6FqGPIvjVtiaIHNDl/OXxLVeri/nS+0gZauAe8B06lrYh0+1DIySoSL1mv/9HkqaE//3yjou8WMBjIttlkSPJbUVZnzbxw1uiwTZCKBXG19bON1ymWk83iFi7bh6fTy7TnmXz0TQuaG";
        RegionEndpoint region = RegionEndpoint.USEast1;
        string bucketName = "myaphogeschoolawss3bucket";
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
            using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
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