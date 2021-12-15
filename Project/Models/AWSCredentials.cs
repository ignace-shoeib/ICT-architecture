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
            @"aws_access_key_id=ASIA5IU4YWEJ2QT2GT5Y
aws_secret_access_key=NRDUBAHxP2P21A3M6qPqVddO8rCKAXz4uZcy7k4e
aws_session_token=FwoGZXIvYXdzEKP//////////wEaDIjtJMO+7vzNUVpzMCLMATy8cj2hcP6dGjFM86fNUnHldTtVCuzg2GQjZG/Go1ue8QkS4JAMqQMgAjd5BpODdott5dn1mMCR9M+oI/nEJ/jrRZM+Z0sclXT6Xb7cvUgIqHS3Zsx1mdwfOaay0duhdaJTAulSOH9tngSeioxwgsR9N3VslqJUeD5nDmehBd7We2Q8LH6jd7AAyHNmxVFL5RbqGUXnFnralZNMbhcr4jVROH+trGhv4/VAW6jFaPb56RIndKAPJ0jSWE5Xpi/2GDnDrP/CDDUh8dW0XSiN2OiNBjIt2tZA4ER2V/6dYbgvVWo0n7WUZI8wzMp2wkT5uP8pFrv1Dxm3JKBE4Ir8ER1H";
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