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
#region BRONNEN
// Connect MySQL Workbench with RDS: https://stackoverflow.com/questions/16488135/unable-to-connect-mysql-workbench-to-rds-instance
#endregion
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
        string mySqlConnectionString = AWSCredentials.MySqlConnectionString.ToString();
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
                        Key = UUID,
                        BucketName = BUCKETNAME,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
            using (AmazonRDSClient client = new AmazonRDSClient(accessKey, secretKey, sessionToken, region))
            {
                using (MySqlConnection conn = new MySqlConnection(mySqlConnectionString))
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
            string fileName = "";
            using (var client = new AmazonS3Client(accessKey, secretKey, sessionToken, region))
            {
                MemoryStream ms = new MemoryStream();
                try
                {
                    using (GetObjectResponse response = await client.GetObjectAsync(BUCKETNAME, key))
                    {
                        response.ResponseStream.CopyTo(ms);
                        contentType = response.Headers.ContentType.ToString();
                    }
                }
                catch (Exception)
                {
                    Response.StatusCode = 404;
                    return File(new byte[0], "text/plain", "");
                }
                msByteArray = ms.ToArray();
            }
            using (AmazonRDSClient client = new AmazonRDSClient(accessKey, secretKey, sessionToken, region))
            {
                using (MySqlConnection conn = new MySqlConnection(mySqlConnectionString))
                {
                    string query = @$"
                    USE Project;
                    SELECT FileName FROM Files
                    WHERE UUID = '{key}';
                    ";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        fileName = dr["FileName"].ToString();
                    }
                    dr.Close();
                    conn.Close();
                }
            }
            return File(msByteArray, contentType, fileName);
        }
    }
}