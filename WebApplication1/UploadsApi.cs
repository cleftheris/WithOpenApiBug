using WebApplication1.Models;

namespace Microsoft.AspNetCore.Builder
{
    public static class UploadsApi
    {
        public static WebApplication MapUploads(this WebApplication app)
        {

            /**
             * Failing 
             */
            app.MapPost("/upload", async (IFormFile file) =>
            {
                var tempFile = Path.GetTempFileName();
                app.Logger.LogInformation(tempFile);
                using var stream = File.OpenWrite(tempFile);
                await file.CopyToAsync(stream);
            })
            .WithOpenApi()
            .WithTags("Issues");

            app.MapPost("/upload_many", async (IFormFileCollection myFiles) =>
            {
                foreach (var file in myFiles)
                {
                    var tempFile = Path.GetTempFileName();
                    app.Logger.LogInformation(tempFile);
                    using var stream = File.OpenWrite(tempFile);
                    await file.CopyToAsync(stream);
                }
            })
            .WithOpenApi()
            .WithTags("Issues");

            /**
             * Workarounds
             */

            app.MapPost("/upload_workaround", async (FileUploadRequest request) =>
            {
                var tempFile = Path.GetTempFileName();
                app.Logger.LogInformation(tempFile);
                using var stream = File.OpenWrite(tempFile);
                await request.File!.CopyToAsync(stream);
            })
            .Accepts<FileUploadRequest>("multipart/form-data").WithOpenApi()
            .WithSummary("This works fine with any file.")
            .WithTags("Workarounds");

            app.MapPost("/upload_workaround_multiple_params", async (CertificateUploadRequest request) =>
            {
                var tempFile = Path.GetTempFileName();
                app.Logger.LogInformation(tempFile);
                using var stream = File.OpenWrite(tempFile);
                await request.File!.CopyToAsync(stream);
            }).Accepts<CertificateUploadRequest>("multipart/form-data")
            .WithOpenApi()
            .WithSummary("This works fine with multiple form files and other form parameters as well.")
            .WithTags("Workarounds");

            app.MapPost("/upload_many_workaround", async (FileCollectionUploadRequest request) =>
            {
                foreach (var file in request.MyFiles!)
                {
                    var tempFile = Path.GetTempFileName();
                    app.Logger.LogInformation(tempFile);
                    using var stream = File.OpenWrite(tempFile);
                    await file.CopyToAsync(stream);
                }
            }).Accepts<FileCollectionUploadRequest>("multipart/form-data")
            .WithOpenApi()
            .WithSummary("This works fine with multiple form files and other form parameters as well.")
            .WithTags("Workarounds");

            return app;
        }
    }
}
