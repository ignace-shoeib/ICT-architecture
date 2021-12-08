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
aws_access_key_id=ASIA5IU4YWEJ4E7653XG
aws_secret_access_key=acITVjhTrt4/4+o8tmPGVmBU2mQcIdRqmGeod2Bb
aws_session_token=FwoGZXIvYXdzEPP//////////wEaDA9XLhPF53SgJOTZLCLMAWLIAXQsoa0XeJ/9Va/kqAPnaALU1lQ3Dgkni6wKsDD/u2WOWcRfyRCRTlfU/1xMJDr4o51ZqIlLNozBuQq/rLNP1NFvbWVMLeRdeux6N0YhebOZ+wTvP5dOUFO4RioJK3GBDWGoG6wizAnBZ54A+hqFAAnblZ9+GLxFCXApNcYDddHyqjEGtX4ZZ+7rZyFjyitWcgjxz8ClkYLlq68kGcG+pl1TN45cwQXoBZ/Wyf18awvFc7CfmCCoEzlTGjUQHSMqwcSvRk/+W9Oalyio8sGNBjItBjqfFLrSfMkDeugE4GXyEJJ8lRpzPh9873JAGJt5eCCHxjlZX92gz3LV/f/X
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
