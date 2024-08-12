namespace MotorRental.Domain.Interfaces
{
    public interface IAwsS3Service
    {
        Task UploadFileAsync(string bucketName, string key, Stream inputStream);
        Task<Stream> GetFileAsync(string bucketName, string key);
        Task DeleteFileAsync(string bucketName, string key);
    }
}
