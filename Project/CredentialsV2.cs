using System;
using System.Collections.Generic;
namespace Project
{
    public class CredentialsV2
    {
        // Plak hier AWS details
        // [Default] NIET MEE KOPIEREN
        static string credentials
            =
            @"
aws_access_key_id=ASIAXJIMXYDLQCWXRH6X
aws_secret_access_key=sflXr/MwnhIWC+NxanOc5sirXeQ/11Mv9MQOzj7v
aws_session_token=FwoGZXIvYXdzEMn//////////wEaDMv8HJ4mQv4Qhf7VWyLOAfzbh/eARR5eiVorfoIFqOr2FFI9Qn8maUwPANY+OKviR08PgwRLbfewSAMNkfNI1jRiE4M1iol0B2BF8r5xKdObNIBYO1Qcb5+L2BKbzdjzBIz2XBJtYq2cbiuG+HgDPcEcm5Y2E0569nii+MauMZe3IXVUioEFYCm61RtbeRMOGNcU0lHVU4hZf61DFS9497TkSTRoZHas8Cz6/NNw8Dfq6U8Qj0zJjWBK6mp6WMPz5mdxZZsL7uLbT0R2fEHweW46JL+w1lx6Z9UVs1+MKM7UuI0GMi1jv48qp6QEg5zHoEVuKGDvRFuPe59Fxv5Tb7vUYp1jGgyHo7fSn/ejbLivzyU=
            ";


        // aws_access_key_id, aws_secret_access_key, aws_session_token opsplitsen
        static string[] credentialsArray = credentials.Trim().Split(new string[] { "\n" }, System.StringSplitOptions.None);

        // access key
        public static string[] access_key = credentialsArray[0].Trim().Split(new String[] { "=" }, 2, StringSplitOptions.None);

        // secret access key
        public static string[] secret_key = credentialsArray[1].Trim().Split(new String[] { "=" }, 2, StringSplitOptions.None);

        // session token
        public static string[] awsSessionToken = credentialsArray[2].Trim().Split(new String[] { "=" }, 2, StringSplitOptions.None);

        public static string AccessKey = String.Concat(Convert.ToString(access_key[1]));
        public static string SecretKey = String.Concat(Convert.ToString(secret_key[1]));
        public static string SessionToken = String.Concat(Convert.ToString(awsSessionToken[1]));
    }
}
