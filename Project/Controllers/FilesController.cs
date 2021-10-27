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
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        private static IAmazonS3 s3Client;
        [HttpPost]
        public async Task<IActionResult> FilesAsync(string fileName, IFormFile file)
        {
            s3Client = new AmazonS3Client(bucketRegion);
            var fileTransferUtility = new TransferUtility(s3Client);
            await fileTransferUtility.UploadAsync((Stream)file, "projectbucket6info", "vxiDNfQXVR49lNGM5mit0tmkad/kR9Sv1s73MCsL");
            return Created("", file);
        }
    }
}
