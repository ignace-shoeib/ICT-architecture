using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using Project.Models;
using System.Linq;
using System.Diagnostics;
using System;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RDSController : ControllerBase
    {
        // Waarschijnlijk binnenkort te verwijderen
        // Omdat dit eerder bij FilesController hoort

        

        // Create Database FileDB
        // Add Table "Files" containing FileID and FileName
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

    }
}
