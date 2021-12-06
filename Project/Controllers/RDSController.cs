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
using System.Diagnostics;
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
        string awsAccessKeyId = CredentialsV2.AccessKey; //AWSCredentials.awsAccessKeyId;//
        string awsSecretAccessKey = CredentialsV2.SecretKey; //AWSCredentials.awsSecretAccessKey; //
        string awsSessionToken = CredentialsV2.SessionToken; // AWSCredentials.awsSessionToken; //
        RegionEndpoint region = AWSCredentials.region;
        string bucketName = AWSCredentials.bucketName;
        #endregion

        // Create Database FileDB
        // Add Table "Files" containing FileID and FileName


     

        #region Upload FILE + Add to database
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {


            string UUID = "";
            using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
            {
                List<FileModel> files = new List<FileModel>();
                string filename = "";

                MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
                conn_string.Server = "127.0.0.1";
                conn_string.UserID = "root";
                conn_string.Password = "root";  // weg commenten als je XAMPP gebruikt i guess
                conn_string.Database = "test";
                conn_string.Port = 3307;        // Verander naar 3306

                using (AmazonRDSClient rds = new AmazonRDSClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
                {



                    using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
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

                        filename = file.FileName;

                        string query = $@"USE FileDB;INSERT INTO Files (FileName) VALUES ('{filename}');";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            try
                            {
                                conn.Open();
                                cmd.ExecuteNonQuery();
                                Debug.WriteLine($"EXECUTED.");
                            }
                            catch (Exception ex)
                            {
                                Debug.WriteLine($"ERROR: {ex.ToString()}");
                            }
                            finally
                            {
                                conn.Close();
                            }
                        }

                        

                        /*
                        try
                        {
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            Debug.WriteLine($"EXECUTED.");
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"ERROR: {ex.ToString()}");
                        }
                        finally
                        {
                            conn.Close();
                        }
                        */


                    }
                }
                
            }
            return Created(UUID, file);
        }

        #endregion

        
        // Als connection niet werkt, voeg jouw IP aan security group toe
        [HttpGet]
        public async Task<IActionResult> CheckConnection()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "kaine-db.cqftybxhj9nh.us-east-1.rds.amazonaws.com";
            conn_string.UserID = "admin";
            conn_string.Password = "rootrootroot";
            //conn_string.Database = "kaine-db";
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
       
    }
}