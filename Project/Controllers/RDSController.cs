using Amazon;
using Amazon.RDS;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
#region BRONNEN
// Connect MySQL Workbench with RDS: https://stackoverflow.com/questions/16488135/unable-to-connect-mysql-workbench-to-rds-instance

#endregion
namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDSController : ControllerBase
    {
        // Waarschijnlijk binnenkort te verwijderen
        // Omdat dit eerder bij FilesController hoort


        #region AWS Connection
        string awsAccessKeyId = AWSCredentials.AccessKey;
        string awsSecretAccessKey = AWSCredentials.SecretKey;
        string awsSessionToken = AWSCredentials.SessionToken;
        RegionEndpoint region = AWSCredentials.region;
        string bucketName = AWSCredentials.BucketName;
        #endregion

        // Create Database FileDB
        // Add Table "Files" containing FileID and FileName
        //bruh fix dit
        //[HttpPost]
        //public async Task<IActionResult> UploadFile()
        //{

        //}
        #region Upload FILE + Add to database

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {


            string UUID = "";
            using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
            {
                List<FileModel> files = new List<FileModel>();

                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                conn_string.Server = "kaine-db.cqftybxhj9nh.us-east-1.rds.amazonaws.com";
                conn_string.UserID = "admin";
                conn_string.Password = "rootrootroot";
                conn_string.Database = "kaine-db";
                conn_string.Port = 3306;

                using (AmazonRDSClient rds = new AmazonRDSClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
                {



                    using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
                    {

                        string filename = String.Concat(Convert.ToString(file.FileName));




                        // UPLOAD FILE GEDEELTE //
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
                        // UPLOAD FILE GEDEELTE

                        string word = "test";

                        string query = $@"
                        INSERT INTO `FileDB.Files` (`FileName`) VALUES (@a);
                        ";

                        MySqlCommand cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("a", word);
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        conn.Close();







                    }
                }

            }
            return Created(UUID, file);
        }

        #endregion
        [HttpGet]
        public async Task<IActionResult> CheckConnection()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "kaine-db.cqftybxhj9nh.us-east-1.rds.amazonaws.com";
            conn_string.UserID = "admin";
            conn_string.Password = "rootrootroot";
            conn_string.Database = "kaine-db";
            conn_string.Port = 3306;

            using (AmazonRDSClient rds = new AmazonRDSClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
            {
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
        }

        // Als connection niet werkt, voeg jouw IP aan security group toe
        // ONDERSTAANDE CODE: ENKEL Query gedeelte werkt, nog geen File toevoeging implementatie
        /*
        [HttpGet]
        public async Task<IActionResult> CheckConnection()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "kaine-db.cqftybxhj9nh.us-east-1.rds.amazonaws.com";
            conn_string.UserID = "admin";
            conn_string.Password = "rootrootroot";
            conn_string.Database = "kaine-db";
            conn_string.Port = 3306;

            using (AmazonRDSClient rds = new AmazonRDSClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
            {
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
        }
        */
    }
}