using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using OpexNow.Api.Configuration;

namespace OpexNow.Api.Controllers;

[ApiController]
[Route("api/documents")]
public class DocumentsController : ControllerBase
{
    private readonly IMinioClient _minio;
    private readonly MinioSettings _settings;

    public DocumentsController(
        IMinioClient minio,
        IOptions<MinioSettings> settings)
    {
        _minio = minio;
        _settings = settings.Value;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> Upload(
        IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("No file.");

        var objectName =
            $"{Guid.NewGuid()}-{file.FileName}";

        using var stream = file.OpenReadStream();

        var putObjectArgs =
            new PutObjectArgs()
                .WithBucket(_settings.BucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(file.Length)
                .WithContentType(file.ContentType);

        await _minio.PutObjectAsync(
            putObjectArgs
        );

        return Ok(new
        {
            fileName = objectName,
            status="Uploaded"
        });
    }
}