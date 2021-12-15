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
            @"aws_access_key_id=ASIAXJIMXYDL62VFBOUX
aws_secret_access_key=bQZhv9bYQ+lHjfexzB0vMj7ItFcY4PfVQeSfzEio
aws_session_token=FwoGZXIvYXdzEJ7//////////wEaDHVbwZYMMFpXR/+h9yLOAeEtGlU90xfVmABqmrej8xmA6/jx/M9adk3xKCna2yfBHkom1heaIAVBSjjAkjphzttcl4ephSbt+qNzLVBySQGx+SGQ0HrqHWKOwavShBTAOsq8LITq7h+uoTZiU+3miIL+Vipv7UliRz5+ObnjCL/1Vis/tywkJFRMYnXs1CIRp0BXlphaRcAre0koGlXE+BcoFsibt+QqYBmbzV/LCIyTIOcGsr9nduoF18M9X8hCgG7CSGXN2UWHCcc15PW8GgqCSWcjgsdFzcDeIecdKL+z540GMi0nyMmMCHANoWr/pt28ZPDoVI2jqFUNYl8x7YX5JwRWczwaBSn8/P51x9o1dFM=";
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
        public const string BucketName = "testbucketkaine1";
        // COGNITO ACCESS
        public const string AppClientId = "5ujhaj6o5l0so8epvalius26m1";
        public const string PoolId = "us-east-1_W33zA1Rbn";
        // DATABASE ACCESS
        public static MySqlConnectionStringBuilder MySqlConnectionString = new MySqlConnectionStringBuilder()
        {
            Server = "kaine-db.cqftybxhj9nh.us-east-1.rds.amazonaws.com",
            UserID = "admin",
            Password = "rootrootroot",
            Database = "Project",
            Port = 3306
        };
    }
}