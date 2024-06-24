using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/dataprocessing")]
    public class DataProcessingController(IUserOperationsService userOperationsService, IFileUploadService fileUploadService) : Controller
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

            if (file.Length > 200_000)
            {
                return BadRequest($"File is to large it must be under 500kb");
            }

            var userIdGuid = new Guid(userId);
            var user = await userOperationsService.GetUserByIdAsync(userIdGuid);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            await fileUploadService.DeleteTransactionAndFileInformationAsync(userIdGuid);
            await fileUploadService.AddFileMetaDataToDatabaseAsync(file, new Guid(userId));
            await fileUploadService.AddTransactionToDatabase(file.OpenReadStream(), user.UserId);

            return Ok();
        }
    }
}
