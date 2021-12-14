using Amazon;
using Amazon.RDS;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        string accessKey = AWSCredentials.AccessKey;
        string secretKey = AWSCredentials.SecretKey;
        string sessionToken = AWSCredentials.SessionToken;
        RegionEndpoint region = AWSCredentials.region;
        const string BUCKETNAME = AWSCredentials.BucketName;
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string UUID = Guid.NewGuid().ToString();
            using (AmazonS3Client client = new AmazonS3Client(accessKey, secretKey, sessionToken, region))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = file.FileName,
                        BucketName = BUCKETNAME,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
            using (AmazonRDSClient client = new AmazonRDSClient(accessKey, secretKey, sessionToken, region))
            {
                using (MySqlConnection conn = new MySqlConnection(AWSCredentials.MySqlConnectionString.ToString()))
                {
                    string query = @$"
                    USE `Project`;
                    INSERT INTO Files(FileName,CreationDate,UUID)
                    VALUES ('{file.FileName}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{UUID}');
                    ";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            return Created(UUID, file);
        }
        [HttpGet]
        public async Task<FileResult> Download(string key)
        {
            byte[] msByteArray;
            string contentType;
            using (var client = new AmazonS3Client(accessKey, secretKey, sessionToken, region))
            {
                MemoryStream ms = new MemoryStream();
                using (GetObjectResponse response = await client.GetObjectAsync(BUCKETNAME, key))
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