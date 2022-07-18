using LoginForm.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO.Compression;
using System.IO;
using System.Collections;

namespace LoginForm.Controllers
{
    public class CompressController : Controller
    {
        [HttpGet]
        public IActionResult CompressFile()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CompressFile(CompressFileViewModel UploadedFile)
        {
            try
            {
                using (var memStream = new MemoryStream())
                {
                    using (var archive = new ZipArchive(memStream, ZipArchiveMode.Create, true))
                    {
                        ZipArchiveEntry zipArchiveEntry = archive.CreateEntry(UploadedFile.UploadFile.FileName, CompressionLevel.Optimal);
                        using (Stream zipStream = zipArchiveEntry.Open())
                        {
                            UploadedFile.UploadFile.CopyTo(zipStream);
                        }
                    }

                    return new FileContentResult(memStream.ToArray(), "application/zip");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
