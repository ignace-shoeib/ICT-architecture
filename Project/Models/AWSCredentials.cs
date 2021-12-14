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
aws_access_key_id=ASIA5IU4YWEJ27534UFD
aws_secret_access_key=jSB/Hp/jVRwrWknef4hPVA+zLmgfPmg4ciTlkrba
aws_session_token=FwoGZXIvYXdzEIz//////////wEaDJwesNaJ+BNVQi20CSLMAReJq2jF+18eOjUKvynRXQoP5shgxu8t4em3RHUztflfBnk/jRZRC+HX6Pz0q3wEqiuD7hQ8D8l0NYdfpkko4nieHvHzwqAIDgXcWkQ6I9yPrG1LutLWBdeYIxigSg+88xVkEwnWPpG/qHOCiievyZsMSk03qzCPCJR9q49NKZTmPIjh5KmW1xX0buos2ndDkR9iBwtEknKqm8T7nIlDB/t/qAr+31jz2mW05QQ6vyWuGMAF/eALvaUoi62qXp2BaDjJSDFamapKQqHQ+CjpvuONBjItV6ivyWeYNXjha4jPfuCSPMSngqCE2JnAvA/biiJ5DT+1t+CMugjuNUFkjTuK";
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