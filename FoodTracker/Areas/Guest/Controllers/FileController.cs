using Microsoft.AspNetCore.Mvc;

namespace FoodTrackerWeb.Areas.Guest.Controllers
{
    [ApiController]
    public class FileController : Controller
    {
        [HttpPost("/upload")]
        public async Task<IActionResult> Upload()
        {
            var files = Request.Form.Files;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Upload");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            foreach (var uploadFile in files)
            {
                var fileName = uploadFile.FileName;
                var filePath = Path.Combine(path, fileName);

                using (var stream = System.IO.File.Create(filePath))
                {
                    await uploadFile.CopyToAsync(stream);
                }

            }

            return Ok();
        }
    }
}
