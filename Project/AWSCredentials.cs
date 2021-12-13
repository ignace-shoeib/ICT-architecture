using Amazon;
using System;
namespace Project
{
    public class AWSCredentials
    {
        // Plak hier AWS details
        // [Default] NIET MEE KOPIEREN
        static string credentials
            =
            @"
aws_access_key_id=ASIA5IU4YWEJX4W32CRW
aws_secret_access_key=8Z6caZpVxqX5AFKJ3DH7b/mWdsl/zGoFM3yia0Ah
aws_session_token=FwoGZXIvYXdzEHQaDBgkKW3Foi28MvfdcyLMAT3/gq+aBewpE/3oCPqm0raTKGfoT7AU9qPbTXvJ43RGxdSicKwNDJB7tw2+UtMG1O5rRKx14z97gWXt9agoY5dAZ+utTGJFD2ewT+Ge9JaMTeQ7bz7qjOTpS0opZVT4uXZsPeqHymR6/clJ1kkevXt4qn4gW0VzdkAYxnZljNViPFwiSHy3tC1/SaWNgxKUKZsDaj6CvLTdWNv89J/EGOCAmBdUHPKG2/xbGToyJo1tWEsWRUlvLgGQklPVRn/iQzriKoR6adyJHv4IOCjnm96NBjIt0MT776+vEHajc07Hj4PfokR3rEMX9rYpfhNL5jwVL4YdYQVzrcGLSm9G8yHD";
        // aws_access_key_id, aws_secret_access_key, aws_session_token opsplitsen
        static string[] credentialsArray = credentials.Trim().Split(new string[] { "\n" }, StringSplitOptions.None);
        // access key
        public static string[] access_key = credentialsArray[0].Trim().Split(new String[] { "=" }, 2, StringSplitOptions.None);
        // secret access key
        public static string[] secret_key = credentialsArray[1].Trim().Split(new String[] { "=" }, 2, StringSplitOptions.None);
        // session token
        public static string[] awsSessionToken = credentialsArray[2].Trim().Split(new String[] { "=" }, 2, StringSplitOptions.None);
        //CLOUD ACCESS
        public static RegionEndpoint region = RegionEndpoint.USEast1;
        public static string AccessKey = String.Concat(Convert.ToString(access_key[1]));
        public static string SecretKey = String.Concat(Convert.ToString(secret_key[1]));
        public static string SessionToken = String.Concat(Convert.ToString(awsSessionToken[1]));
        //BUCKET ACCESS
        public static string bucketName = "myaphogeschoolawss3bucket";
        //COGNITO ACCESS
        public static string appClientId = "6ke8vkca9e4lqhc787orsiocec";
        public static string poolId = "us-east-1_y1Etub0Qe";
    }
}