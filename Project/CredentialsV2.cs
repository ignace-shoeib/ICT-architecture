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
aws_access_key_id=ASIA5IU4YWEJTGXKTRZA
aws_secret_access_key=UAu0KOmI5za7mMnP81n/S76zI7Lr2nSl8ekRF0HO
aws_session_token=FwoGZXIvYXdzEHEaDNsazd9kQmUnbJCngCLMAdnOb5EFD0q5Iv4slMf4VOxWPZuN/EIP0xLfVCUvG1H7cD2/xf8TVxVxyivW5jBG2d3xx8p/o2mxxgtWRzSECTiP54eoP7HcfSmQe/Fhevlum6wckhL8ohOO9PgJ/2AsgdOF1PwYjj7KeYtvFRkXm0dnkVgUl6kVb4R7tiKMz/WLN3HhEMhQsLgM/bTIEgEnlGZ82JMpUMT+SYnFWMD7hw9NWTNLqZP4SCbY1GQ6I7JEictJheEbtyIXqioW+dpT9n5zxuyLqKpF/lzSVCiN0N2NBjIt6FUyGCvojTkNH94leNk+QbCqnebjB8FOrZAo3FQcyMRoBoLnDCKi7S9xbuTD";


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
