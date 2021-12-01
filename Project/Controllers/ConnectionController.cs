﻿using Amazon;
using Amazon.EC2;
using Amazon.EC2.Model;
using Amazon.EC2.Util;
using Amazon.RDS;
using Amazon.RDS.Model;
using Amazon.RDS.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionController : ControllerBase
    {
        string awsAccessKeyId = AWSCredentials.awsAccessKeyId;
        string awsSecretAccessKey = AWSCredentials.awsSecretAccessKey;
        string awsSessionToken = AWSCredentials.awsSessionToken;
        RegionEndpoint region = AWSCredentials.region;

        [HttpGet]
        public async Task<ActionResult> CheckConnection()
        {
            string result = "";

            // CHECK EC2
            try
            {
                var ec2 = new AmazonEC2Client(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region);
                result = result + "[SUCCESS]: CONNECTION WITH EC2 AVAILABLE\n";
                bool done = false;
                var InstanceIDs = new List<string>();
                var request = new DescribeInstancesRequest();
                while (!done)
                {
                    DescribeInstancesResponse response = await ec2.DescribeInstancesAsync(request);
                    foreach (Reservation reservation in response.Reservations)
                    {
                        foreach(Instance instance in reservation.Instances)
                        {
                            InstanceIDs.Add(instance.InstanceId);
                            result = result + instance.InstanceId.ToString() + "\n";
                        }
                    }

                    request.NextToken = response.NextToken;
                    if (response.NextToken == null)
                    {
                        done = true;
                    }

                }

                

            }
            catch (AmazonEC2Exception e)
            {
                result = result + "[ERROR]: " + e.Message + "\n";
            }


            // CHECK RDS

            try
            {
                var rds = new AmazonRDSClient(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region);
                
                result = result + "[SUCCESS]: CONNECTION WITH EC2 AVAILABLE\n";

               


            }
            catch (AmazonRDSException e)
            {
                result = result + "[ERROR]: " + e.ToString() + "\n";
            }
            
            return Ok(result);
        }
    }
}
