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
            @"aws_access_key_id=ASIA5IU4YWEJTO644VVQ
aws_secret_access_key=dPZqDbDThv2g+O9ptlTvppwT+wr7BOQZ6VtMyqzS
aws_session_token=FwoGZXIvYXdzELj//////////wEaDCv+Ix2nbVS/k+DdiyLMAVwcwhsens8qWwxiFJztibSxYk5IjXQCishh8u2Z1dH3LYw4BdUufGQp1XZ+NXCKXxj6MxzgHivde3z4M/X57bSB/6eMtdDXQxld2fPh7u5FItrjbrUo326jaAP+wYLDsAJ3d43A5Ht8OwecB5f8tOvxx7o6TbJoHF6PRWSdZroz71OzjmhHoyn2LaCI5dyJMWi4DmC/6JQVWT4vvrVUvsA+bi/8l3TNx5THxItwLyzhWHmujChwSPjknVxdmx5nXGXwlS2PeQ552QkwSyi7ne2NBjItocC2qK0g0tysUnI57Dq9D9IH0G/zfi2QUDsYtWx/GnUZeJRHRkjh6QpEin7B";
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