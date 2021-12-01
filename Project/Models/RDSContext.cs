using Microsoft.EntityFrameworkCore;
#region BRONNEN
// DBContext        https://www.youtube.com/watch?v=qkJ9keBmQWo&t
#endregion
namespace Project.Models
{
    public class RDSContext : DbContext
    {
        /*
        public string getConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;


            string dbname = appConfig["RDS_DB_NAME"];
            string username = appConfig["RDS_USERNAME"];
            string password = appConfig["RDS_PASSWORD"];
            string hostname = appConfig["RDS_HOSTNAME"];
            string port = appConfig["RDS_PORT"];

            string connectionString = $"Data Source={hostname};Initial Catalog={dbname};User ID={username};Password={password};";

            return connectionString;
        }
        */
        public RDSContext(DbContextOptions options) : base(options) { }
        public virtual DbSet<FileModel> Files { get; set; }
    }
}