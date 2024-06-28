using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/dataprocessing")]
    public class FileHandlingController(IUserOperationsService userOperationsService, IFileUploadService fileUploadService) : Controller
    {
        [HttpPost("fileUpload")]
        public async Task<ActionResult> FileUpload(IFormFile file, string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (file.ContentType != "text/csv")
            {
                return BadRequest("Invaild FileType Please Only Upload CSV format");
            }

            if (file.Length > 500_000)
            {
                return BadRequest($"File is to large it must be under 500kb");
            }

            var user = await userOperationsService.GetUserByIdAsync(new Guid(userId));
            if (user == null)
            {
                return BadRequest("User not found");
            }

            await fileUploadService.DeleteTransactionsByIdAsync(new Guid(userId));
            await fileUploadService.AddFileMetaDataToDatabaseAsync(file, new Guid(userId));
            await fileUploadService.AddTransactionToDatabase(file.OpenReadStream(), user.UserId);

            return Ok();
        }
    }
}
