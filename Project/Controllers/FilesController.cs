﻿using Amazon;
using Amazon.RDS;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using MySql.Data.MySqlClient;
using Project.Models;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
#region BRONNEN
// Connect MySQL Workbench with RDS:    https://stackoverflow.com/questions/16488135/unable-to-connect-mysql-workbench-to-rds-instance
// Files                                https://stackoverflow.com/questions/1690337/what-is-the-best-practice-for-storing-a-file-upload-to-a-memorystream-c
//                                      https://stackoverflow.com/questions/19187964/how-to-upload-memory-file-to-amazon-s3
// RDS - SQL                            https://www.w3schools.com/sql/sql_insert.asp
// 
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
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            string UUID = Guid.NewGuid().ToString();
            using (AmazonS3Client client = new AmazonS3Client(accessKey, secretKey, sessionToken, region))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    if (file == null)
                    {
                        return BadRequest();
                    }
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
                    newMemoryStream.Dispose();
                }
            }
            using (AmazonRDSClient client = new AmazonRDSClient(accessKey, secretKey, sessionToken, region))
            {
                await using (MySqlConnection conn = new MySqlConnection(mySqlConnectionString))
                {
                    string query = @$"
                    USE `Project`;
                    INSERT INTO Files(FileName,CreationDate,UUID)
                    VALUES ('{file.FileName}','{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}','{UUID}');
                    ";
                    await conn.OpenAsync();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    await cmd.ExecuteNonQueryAsync();
                    await conn.CloseAsync();
                }
            }
            return Created("", "{\n  \"UUID\": \"" + UUID + "\"\n}");
        }
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<FileResult> Download(string key)
        {
            return await DownloadMethod(key);
        }
        [Route("{key}")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<FileResult> DownloadFromRoute([FromRoute] string key)
        {
            return await DownloadMethod(key);
        }
        private async Task<FileResult> DownloadMethod(string key)
        {
            MemoryStream ms = new MemoryStream();
            string fileName = "";
            string checksum = "";
            using (var client = new AmazonS3Client(accessKey, secretKey, sessionToken, region))
            {
                try
                {
                    GetObjectResponse response = await client.GetObjectAsync(BUCKETNAME, key);
                    await response.ResponseStream.CopyToAsync(ms);
                }
                catch (Exception)
                {
                    Response.StatusCode = 404;
                    return File(new byte[0], "text/plain", "");
                }
            }
            using (AmazonRDSClient client = new AmazonRDSClient(accessKey, secretKey, sessionToken, region))
            {
                using (MySqlConnection conn = new MySqlConnection(mySqlConnectionString))
                {
                    string query = @$"
                    USE Project;
                    SELECT FileName,Checksum FROM Files
                    WHERE UUID = '{key}';
                    ";
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    MySqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        fileName = dr["FileName"].ToString();
                        checksum = dr["Checksum"].ToString();
                    }
                    dr.Close();
                    conn.Close();
                }
            }
            ms.Seek(0, SeekOrigin.Begin);
            Response.Headers.Add("Checksum", new StringValues(checksum));
            return File(ms, "application/octet-stream", fileName);
        }
    }
}