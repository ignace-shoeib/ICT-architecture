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
            @"aws_access_key_id=ASIAXJIMXYDL7WGO3RB5
aws_secret_access_key=ywp0FYX4LsYrqt9pHKOxL9v6Uf+RNTRsxo4IKtnI
aws_session_token=FwoGZXIvYXdzEJv//////////wEaDKrt/6hFzx70IEVexCLOAYTJK4J5iTZi/URYD6d5Lrw+DHMCenVLMuHIEICFyB08mB1dwFQRc0u1dN/AZyM9Td6IIQV8CDg8FsiUFq+pTKoD9ZkgrM/BdHaDhgg7mwPvTl4BRzAc9Qu0CsshUeqXyYgEl/E/a3Z9+9wdFfeKSqMBm1BNnt9qKAoZat/8Tm2OFPRRFBR+4VYLHGDjCI6W5xAhJgB0azN0xlVx+kNyzgjEBoKyAG2YDD/7BRnAUDJfbhYi2U6M7tyeRjN1jpQLO1seug7z77t+K49dXMBwKNjv5o0GMi1FzzLDZRL93ApTWJ2hHSQ173a14TNe8QW0/He36oMla4s8wMNOGWaEChIYwp8=";
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