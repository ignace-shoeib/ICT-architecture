using Amazon;
using System;
using MySql.Data.MySqlClient;
namespace Project
{
    public class AWSCredentials
    {
        // Plak hier AWS details
        // [Default] NIET MEE KOPIEREN
        const string credentials
            =
            @"
aws_access_key_id=ASIA5IU4YWEJZOGJXIPC
aws_secret_access_key=exysSihw81frNyVHL1EUXjyHDzZMDVMNHLkUlxgY
aws_session_token=FwoGZXIvYXdzEIf//////////wEaDOMq8MXnWzEAZZaH2yLMAWqr4vO8+u1wKjXZlyal19ZQ72rObLWHJ+biziEMCZropm3tf+EgxuGsoSYD91PItU1ACB2FwUOM/5Cw2/TMKZGmWZRVR9FYbQOfXjyf7PKJkjTnMeeKanda/xVpzcx8NAcLNW5ktjdyR4p5RjEaqCqMQiP+EClI5+GaLIq6Nmr61w5uyENhEFPqJK6KV3KWmT5D2p37TVsdpn1d1QUwShG4Y0EFgP9tEfhHsUfhC71NhNi+Ujp7MVT76sEsMiBuFJ2narOOZOGryB2bzSiEv+KNBjItlvcHtKz9lQab0SkVq7smj75CtsoO5h0WOlxrc9Mm0umvaW2Cy5J5xWqk+Ztg";
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