using System.Collections.Generic;
using DiegoRangel.DotNet.Framework.CQRS.API.ViewModels.BinaryObjects;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.IO;
using Microsoft.AspNetCore.Http;

namespace DiegoRangel.DotNet.Framework.CQRS.API.Services.IO
{
    public interface IFilesService
    {
        BinaryObjectInputViewModel UploadTempFile(IFormFile file);
        BinaryObject UploadImage(BinaryObjectInputViewModel input);
        BinaryObject GetUploadedFile(BinaryObjectInputViewModel input);
        void DeleteTempFile(string tempFileName);
        void DeleteTempFiles(IList<BinaryObjectInputViewModel> inputs);
    }
}