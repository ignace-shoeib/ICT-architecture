using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Amazon.S3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3.Transfer;
using System.IO;
namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        string awsAccessKeyId = "ASIA5IU4YWEJT3RT4XUD";
        string awsSecretAccessKey = "3WEaFdC8Wsu2sce9Rgh8jSzHdKXJS7WCZwDhe6iE";
        string awsSessionToken = "FwoGZXIvYXdzEKD//////////wEaDJXm00rBFQBogAntVSLMAR9zU+0V9d+oCCDnaU5Q6Fs9HtkRDfCuHfbFzZBenC1Ocskm8sGT/RB0I1rmKFlenOmijdy1YSyEJtaYmbq0Lee6A74q8tRR6edwXgfcB/mvPh0yzgIb0ODoYmsppgA/pWiXyDxLBljmV7S7qsT3CSCoMco4g5kB5l7GMs34vllnad3+3Z6PriYw+a/4E3PghgQSKysMu3g3RtMah9ZkDkmyqFmtBtaj/54G1rk1FNHXIS9UahvB64cZxMp78XAsOiGZyZ5ETwQsCq5RECirpL+MBjIt++0GdvDYabGPSr600w0u7cetxJMuXjVXDyknbMefs0S0ouGTB66zu2v30+U2";
        RegionEndpoint region = RegionEndpoint.USEast1;
        string bucketName = "myaphogeschoolawss3bucket";
        [HttpPost]
        public async Task<IActionResult> Upload(string fileName, IFormFile file)
        {
            using (var client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region))
            {
                using (var newMemoryStream = new MemoryStream())
                {
                    file.CopyTo(newMemoryStream);

                    var uploadRequest = new TransferUtilityUploadRequest
                    {
                        InputStream = newMemoryStream,
                        Key = fileName,
                        BucketName = bucketName,
                        CannedACL = S3CannedACL.PublicRead
                    };
                    var fileTransferUtility = new TransferUtility(client);
                    await fileTransferUtility.UploadAsync(uploadRequest);
                }
            }
            return Created("", file);
        }
        [HttpGet]
        public IActionResult Download(string fileName)
        {
            return Ok(fileName);
        }
    }
}