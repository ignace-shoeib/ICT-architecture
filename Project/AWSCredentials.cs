using Amazon;
using MySql.Data.MySqlClient;
using System;
namespace Project
{
    public class AWSCredentials
    {
        // Plak hier AWS details
        // [Default] NIET MEE KOPIEREN
        const string credentials
            =
            @"
aws_access_key_id=ASIA5IU4YWEJ2C4E3IR4
aws_secret_access_key=BoPue6TgXG5I/Z0BRJWAS9DT8FEL1mdtIaAPaJYs
aws_session_token=FwoGZXIvYXdzEIv//////////wEaDH8SdQ0B7gsJx+wMOCLMAf9sWWeFHBIqenrnRAxs05LE6ImxePUOiek+OKk49Ezj77iOqUgtL/0ATwJy7ua3KWDUNwV193M8HSALg3TVk0DsEPNwjoOEszWH3U7NEjyQw06sm2XQp0Ey5AUv1MpPrI4b7zFg6foisFjxPfZmlEn1mjUSsmo/fKT3op9dEsqUcg0BIQp5FRO1Fw42PaCW37WUaNCHIvUs+l/cFJi9wBZ4SPPPVGZt/M8UcTfsvJVkznecAWCxaZE4th9h+Ij3oT0W5rovxwx3VdJSnyj/s+ONBjItwz1wqYZ92RUm58CDGSlj1rMDnEf9srWQEda4gJyvKgEV60H8QWgerytOstqr";
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