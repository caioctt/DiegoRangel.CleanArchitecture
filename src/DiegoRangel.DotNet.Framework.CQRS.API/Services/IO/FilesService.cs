using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using DiegoRangel.DotNet.Framework.CQRS.API.ViewModels.BinaryObjects;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Services.IO
{
    public class FilesService : IFilesService
    {
        private const string TempFolderName = "Temp\\Upload";
        private readonly IHostingEnvironment _hostingEnvironment;

        public FilesService(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public BinaryObjectInputViewModel UploadTempFile(IFormFile file)
        {
            var tempUploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, TempFolderName);

            if (!Directory.Exists(tempUploadFolder))
                Directory.CreateDirectory(tempUploadFolder);

            var fileNameSplited = file.FileName.Split('.');
            var extension = fileNameSplited[fileNameSplited.Length - 1];
            var tempFileName = $"{Guid.NewGuid().ToString()}.{extension}";
            var fullPath = Path.Combine(tempUploadFolder, tempFileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
                file.CopyTo(stream);

            return new BinaryObjectInputViewModel
            {
                TempFileName = tempFileName,
                Name = file.FileName,
                Extension = extension.ToLower(),
                ContentType = file.ContentType,
                SizeInBytes = file.Length
            };
        }

        public BinaryObject UploadImage(BinaryObjectInputViewModel input)
        {
            if (input == null) return null;

            var mime = GetMimeTypeFromBase64(input.Base64Url);
            var img = new BinaryObject
            {
                Id = input.Id ?? 0,
                Name = Guid.NewGuid().ToString(),
                Extension = mime.Split("/")[1],
                ContentType = mime
            };
            var base64Url = Regex.Replace(input.Base64Url, @"^data:image\/[a-zA-Z]+;base64,", string.Empty);
            img.Stream = Convert.FromBase64String(base64Url);
            img.SizeInBytes = img.Stream.Length;

            return img;
        }

        public BinaryObject GetUploadedFile(BinaryObjectInputViewModel input)
        {
            var tempUploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, TempFolderName);
            var tempUploadFilePath = Path.Combine(tempUploadFolder, input.TempFileName);

            return new BinaryObject
            {
                Name = input.Name,
                ContentType = input.ContentType,
                Extension = input.Extension,
                SizeInBytes = input.SizeInBytes,
                Stream = File.ReadAllBytes(tempUploadFilePath)
            };
        }

        public void DeleteTempFile(string tempFileName)
        {
            var tempUploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, TempFolderName);
            var tempUploadFilePath = Path.Combine(tempUploadFolder, tempFileName);

            if (!File.Exists(tempUploadFilePath)) return;

            File.Delete(tempUploadFilePath);
        }

        public void DeleteTempFiles(IList<BinaryObjectInputViewModel> inputs)
        {
            if (inputs == null || inputs.Count == 0) return;

            foreach (var input in inputs)
                DeleteTempFile(input.TempFileName);
        }

        private string GetMimeTypeFromBase64(string base64Url)
        {
            if (string.IsNullOrEmpty(base64Url)) return null;

            var regex = new Regex(@"data:(?<mime>[\w/\-\.]+);(?<encoding>\w+),(?<data>.*)", RegexOptions.Compiled);
            var match = regex.Match(base64Url);
            return match.Groups["mime"].Value;
        }
    }
}