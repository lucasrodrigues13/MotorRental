using Amazon.S3;
using Amazon.S3.Model;
using MotorRental.Domain.Interfaces;

namespace MotorRental.Infrastructure.ExternalServices
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IAmazonS3 _s3Client;
        public AwsS3Service(IAmazonS3 s3Client)
        {
            _s3Client = s3Client;
        }

        public async Task UploadFileAsync(string bucketName, string key, Stream inputStream)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = inputStream
            };

            await _s3Client.PutObjectAsync(putRequest);
        }

        public async Task<Stream> GetFileAsync(string bucketName, string key)
        {
            var getRequest = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            var response = await _s3Client.GetObjectAsync(getRequest);
            return response.ResponseStream;
        }

        public async Task DeleteFileAsync(string bucketName, string key)
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
        }
    }
}
