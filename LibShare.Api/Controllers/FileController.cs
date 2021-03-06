﻿using LibShare.Api.Data.Interfaces.IRepositories;
using Microsoft.AspNetCore.Authorization;
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
    //[Authorize]
    public class FileController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IBookRepository _bookRepo;

        public FileController(IConfiguration configuration, IBookRepository bookRepo)
        {
            _configuration = configuration;
            _bookRepo = bookRepo;
        }

        [HttpPost, DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromQuery] string bookId)
        {
            var formCollection = await Request.ReadFormAsync();
            var file = formCollection.Files.First();
            var folderName = _configuration.GetValue<string>("BooksPath");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (file.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');

                var extension = Path.GetExtension(fileName);
                fileName = bookId + Guid.NewGuid().ToString() + extension;
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }


                return Ok(new { dbPath });
            }
            else
            {
                return BadRequest();
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
