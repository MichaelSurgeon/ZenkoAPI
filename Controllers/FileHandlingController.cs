using Microsoft.AspNetCore.Mvc;
using ZenkoAPI.Dtos;
using ZenkoAPI.Models;
using ZenkoAPI.Services;

namespace ZenkoAPI.Controllers
{
    [ApiController]
    [Route("/api/file")]
    public class FileHandlingController(IUserOperationsService userOperationsService, IFileUploadService fileUploadService) : Controller
    {
        [HttpPost("uploadFile")]
        public async Task<ActionResult> FileUpload(IFormFile file, string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (file.ContentType != "text/csv")
            {
                return BadRequest();
            }

            if (file.Length > 500_000)
            {
                return BadRequest();
            }

            var user = await userOperationsService.GetUserByIdAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            await fileUploadService.DeleteTransactionsByIdAsync(new Guid(userId));
            await fileUploadService.AddFileMetaDataToDatabaseAsync(file, new Guid(userId));
            await fileUploadService.ParseAndAddTransactionToDatabase(file.OpenReadStream(), user.UserId);

            return Ok();
        }

        [HttpGet("getFileData")]
        public async Task<ActionResult<List<FileDataDto>>> GetFileData(string userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await userOperationsService.GetUserByIdAsync(new Guid(userId));
            if (user == null)
            {
                return NotFound();
            }

            var result = await fileUploadService.GetFileUploadInformationAsync(new Guid(userId));
            if (result == null)
            {
                return NotFound();
            }

            var returnObject = result.Select(x => new FileDataDto(x.FileSize.ToString(), x.FileName, x.UploadTime.ToString()))
                                     .OrderByDescending(x => x.FileDate).ToList();
            return returnObject;
        }
    }
}
