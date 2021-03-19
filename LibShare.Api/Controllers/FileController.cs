using LibShare.Api.Data.ApiModels;
using LibShare.Api.Data.ApiModels.ResponseApiModels;
using LibShare.Api.Data.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace LibShare.Api.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBookRepository _bookRepo;
        private readonly IWebHostEnvironment _env;

        public FileController(IConfiguration configuration, IBookRepository bookRepo, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _bookRepo = bookRepo;
            _env = env;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        [Authorize]
        public async Task<IActionResult> Upload([FromQuery] string bookId)
        {
            if (bookId == null)
                return BadRequest("Потрібно ID книги");
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();
            var folderName = _configuration.GetValue<string>("BooksPath");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            var book = await _bookRepo.GetByIdAsync(bookId);

            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                var extension = Path.GetExtension(fileName);
                fileName = Guid.NewGuid().ToString() + extension;
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                book.File = fileName;
                await _bookRepo.UpdateAsync(book);
                return Ok(new { dbPath });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload-image")]
        [Authorize]
        public async Task<IActionResult> UploadImageBook([FromBody] ImageApiModel image, [FromQuery] string bookId)
        {
            if (bookId == null)
                return BadRequest(new MessageApiModel() { Message = "Потрібно ID книги" });

            var book = await _bookRepo.GetByIdAsync(bookId);

            if (image.Photo != null)
            {
                string base64 = image.Photo;
                if (base64.Contains(","))
                {
                    base64 = base64.Split(',')[1];
                }

                var serverPath = _env.ContentRootPath;
                var folderName = _configuration.GetValue<string>("BooksPath");
                var path = Path.Combine(serverPath, folderName);

                if (!Directory.Exists(path)) { Directory.CreateDirectory(path); }

                string ext = ".jpg";
                string fileName = Guid.NewGuid().ToString("D") + ext;
                string filePathSave = Path.Combine(path, fileName);

                //Convert Base64 Encoded string to Byte Array.
                byte[] imageBytes = Convert.FromBase64String(base64);
                System.IO.File.WriteAllBytes(filePathSave, imageBytes);

                book.Image = fileName;
                await _bookRepo.UpdateAsync(book);
                return Ok(new MessageApiModel { Message = fileName });
            }
            else
            {
                return BadRequest(new MessageApiModel() { Message = "Потрібно фото" });
            }
        }

        [HttpGet, DisableRequestSizeLimit]
        [Route("download")]
        public async Task<IActionResult> Download([FromQuery] string bookId)
        {
            var book = await _bookRepo.GetByIdAsync(bookId);
            if (book == null)
                return NotFound();
            var folderName = _configuration.GetValue<string>("BooksPath");
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), folderName, book.File);
            if (!System.IO.File.Exists(filePath))
                return NotFound();
            var memory = new MemoryStream();
            await using (var stream = new FileStream(filePath, FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(filePath), book.File);
        }

        private string GetContentType(string path)
        {
            var provider = new FileExtensionContentTypeProvider();
            string contentType;

            if (!provider.TryGetContentType(path, out contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
