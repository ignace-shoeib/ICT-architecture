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

        

        [HttpGet]
        public IActionResult Index()
        {
            MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();
            conn_string.Server = "127.0.0.1";
            conn_string.UserID = "root";
            conn_string.Password = "root";
            conn_string.Database = "test";
            conn_string.Port = 3307; // Gewoonlijk 3306 

            List<FileModel> files = new List<FileModel>();
            using (MySqlConnection conn = new MySqlConnection(conn_string.ToString()))
            {
                MySqlCommand cmd = new MySqlCommand("SELECT * FROM Files", conn);
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
