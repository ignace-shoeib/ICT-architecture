using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Project.Models;
using System.Linq;
using System.Diagnostics;
using System;
using Amazon;
using Amazon.S3;
using System.Threading.Tasks;
using System.IO;
using Amazon.S3.Transfer;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDSController : ControllerBase
    {
        // Waarschijnlijk binnenkort te verwijderen
        // Omdat dit eerder bij FilesController hoort


        #region AWS Connection
        string awsAccessKeyId = AWSCredentials.awsAccessKeyId;
        string awsSecretAccessKey = AWSCredentials.awsSecretAccessKey;
        string awsSessionToken = AWSCredentials.awsSessionToken;
        RegionEndpoint region = AWSCredentials.region;
        string bucketName = AWSCredentials.bucketName;
        #endregion

        // Create Database FileDB
        // Add Table "Files" containing FileID and FileName

        #region Create FileDB
        [HttpGet]
        public IActionResult CreateFileDB()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "127.0.0.1";
            conn_string.UserID = "root";
            conn_string.Password = "root";  // weg commenten als je XAMPP gebruikt i guess
            conn_string.Database = "test";
            conn_string.Port = 3307;        // Verander naar 3306 als je geen gehandicapte installatie hebt zoals mij

            List<FileModel> files = new List<FileModel>();
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {

                string query = @"
                CREATE DATABASE IF NOT EXISTS `FileDB`;
                USE `FileDB`;
                CREATE TABLE IF NOT EXISTS `Files`
                (
                    FileID      INT(6) UNSIGNED AUTO_INCREMENT PRIMARY KEY,
                    FileName    VARCHAR(30) NOT NULL
                );
                SELECT * FROM `FileDB`.`Files`;

                                ";
                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    FileModel file = new FileModel();
                    file.FileID = Convert.ToInt32(dr["File_ID"]);
                    file.FileName = dr["File_Name"].ToString();


                    files.Add(file);
                }
                dr.Close();
            }

            return Ok(files);
        }
        #endregion



        #region Upload FILE + Add to database
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

        #endregion
    }




}
