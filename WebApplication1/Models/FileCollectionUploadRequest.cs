using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace WebApplication1.Models;
/// <summary>File upload request</summary>
public class FileCollectionUploadRequest
{
    /// <summary>File data</summary>
    [Required]
    public IFormFileCollection? MyFiles { get; set; }

    /// <summary>
    /// Bind method
    /// </summary>
    /// <param name="context"></param>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public static async ValueTask<FileCollectionUploadRequest> BindAsync(HttpContext context,
                                                   ParameterInfo parameter)
    {
        var form = await context.Request.ReadFormAsync();
        var files = form.Files;
        return new FileCollectionUploadRequest
        {
            MyFiles = files
        };
    }
}
