using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
#region BRONNEN
// AmazonEC2Client:     https://docs.aws.amazon.com/sdkfornet/v3/apidocs/items/EC2/TEC2Client.html
//                      https://s3.cn-north-1.amazonaws.com.cn/aws-dam-prod/china/pdf/aws-sdk-net-dg.pdf
// Tags - resourcetype  https://sdk.amazonaws.com/java/api/latest/software/amazon/awssdk/services/ec2/model/ResourceType.html#INSTANCE
// Tags                 https://stackoverflow.com/questions/30337918/example-of-programmatically-creating-restoring-an-ebs-snapshot-using-aws-sdk                     
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
        #region CREATE INSTANCE
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

                    // Adds name to instance
                    TagSpecifications = new List<TagSpecification>
                    {
                        new TagSpecification()
                        {
                            Tags = new List<Tag>
                            {
                                {
                                    new Tag()
                                    {
                                        Key = "Name",
                                        Value = instanceName
                                    }
                                }
                            }, ResourceType = "INSTANCE"
                        }
                    }
                };

                var launch = ec2.RunInstancesAsync(request);
                RunInstancesResponse requestlaunch = await launch;
                return Ok(requestlaunch);
            }

        }
        #endregion
    }
}