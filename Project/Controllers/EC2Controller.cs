using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
//amazon services
using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;


#region BRONNEN
// AmazonEC2Client:     https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html
//                      https://s3.cn-north-1.amazonaws.com.cn/aws-dam-prod/china/pdf/aws-sdk-net-dg.pdf

#endregion

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EC2Controller : ControllerBase
    {
        string awsAccessKeyId = AWSCredentials.awsAccessKeyId;
        string awsSecretAccessKey = AWSCredentials.awsSecretAccessKey;
        string awsSessionToken = AWSCredentials.awsSessionToken;
        RegionEndpoint region = AWSCredentials.region;

        [HttpPost]
        public async Task<IActionResult> createInstance(string instanceName)
        {
            using (var ec2 = new AmazonEC2Client(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
            {
                var request = new RunInstancesRequest()
                {
                    InstanceType = InstanceType.T2Micro,
                    MinCount = 1,
                    MaxCount = 1,
                    ImageId = "ami-04902260ca3d33422", // Amazon Linux 2 AMI (HVM) - Kernel 5.10 
                };


                var launch = ec2.RunInstancesAsync(request);
                RunInstancesResponse requestlaunch = await launch;
                return Ok(requestlaunch);
            }

        }

       
    }

}
