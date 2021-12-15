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
            @"aws_access_key_id=ASIA5IU4YWEJUGYBQTEN
aws_secret_access_key=5dfYQ5VwBQQLSP86IV2BMwZX/xdnjkrM6iFPdQHg
aws_session_token=FwoGZXIvYXdzEKf//////////wEaDKr4i3p5LjMnIhLGTiLMAbGNutqWNeolw89wt76OWf4Qo0tqtpXpouIKBrm4EBmI16dX4i8Ae4F5y9KxWCSF99aUypf3F91awr1hx9ee1VrgEZEUQ+T0nwDxxtQFe1uGB8TwvhGpGFoqOytSIyKVLFqIBUkSNC976QAzCNDHfLT9WOFLrz7FGJHnykpnL1ExIf5Yhc1mDoOkPI10eRn/wZZGUtEtXgY3ZGKYhq+bMaPNoL/PxREOmStD/oaMekcCXni4SfbiFUFnylqXO4MtbNju1nPZ6zjFPV1jcSiezOmNBjIt/QYuA/UIwWe7POl97q/UZjhJzx8G+rSbQHXp9JEleOSnUTeFq5ajCxa0jICv";
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