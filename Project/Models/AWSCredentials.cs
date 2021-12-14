using Amazon;
using MySql.Data.MySqlClient;
using System;
namespace Project.Models
{
    public class AWSCredentials
    {
        // Plak hier AWS details
        // [Default] NIET MEE KOPIEREN
        const string credentials
            =
            @"
aws_access_key_id=ASIA5IU4YWEJVLJJM7ZS
aws_secret_access_key=X5Zemw10G4LWlN/bPg8fYbB5K1ND9tf44pOhSWRX
aws_session_token=FwoGZXIvYXdzEI7//////////wEaDH39ShDqCzk+mpdjWyLMAcJ9ugmsmI151kDT6vSEEqFfyqsGsUM6Pt9161zJStnZis5QTHpwoWEnGNUYtxfczDYn3/qdL3S2ST3HsvK42yhKygCKtPHidQMifVdIShlOmlv0r7sYc9FjboxBnDqc+9aTTgs2aE4423I9fGeZbx8Qzvacyxm9Hog5XXU6dQFMx1DE4e0q6HXAlaM0nnI2xJmO3w/rInECuaKX2YxqKYGtkDCNdKJVPuVGAX3Iti4nlgQZAH1MCGJRdnRkDge+1LjTrd9zNkFVr8Q2tijXgOSNBjItt/C8gzAebCspJzIlpliUL9qoVvBsFrPn6iCWNtMFhNhljn8KAWLrJ1yCFtpV";
        // aws_access_key_id, aws_secret_access_key, aws_session_token opsplitsen
        static string[] credentialsArray = credentials.Trim().Split(new string[] { "\n" }, StringSplitOptions.None);
        // access key
        public static string[] access_key = credentialsArray[0].Trim().Split(new String[] { "=" }, 2, StringSplitOptions.None);
        // secret access key
        public static string[] secret_key = credentialsArray[1].Trim().Split(new String[] { "=" }, 2, StringSplitOptions.None);
        // session token
        public static string[] awsSessionToken = credentialsArray[2].Trim().Split(new String[] { "=" }, 2, StringSplitOptions.None);
        // CLOUD ACCESS
        public static RegionEndpoint region = RegionEndpoint.USEast1;
        public static string AccessKey = String.Concat(Convert.ToString(access_key[1]));
        public static string SecretKey = String.Concat(Convert.ToString(secret_key[1]));
        public static string SessionToken = String.Concat(Convert.ToString(awsSessionToken[1]));
        // BUCKET ACCESS
        public const string BucketName = "myaphogeschoolawss3bucket";
        // COGNITO ACCESS
        public const string AppClientId = "6ke8vkca9e4lqhc787orsiocec";
        public const string PoolId = "us-east-1_y1Etub0Qe";
        // DATABASE ACCESS
        public static MySqlConnectionStringBuilder MySqlConnectionString = new MySqlConnectionStringBuilder()
        {
            Server = "project-db.cd4zrs9jaq6a.us-east-1.rds.amazonaws.com",
            UserID = "admin",
            Password = "rootrootroot",
            Database = "Project",
            Port = 3306
        };
    }
}