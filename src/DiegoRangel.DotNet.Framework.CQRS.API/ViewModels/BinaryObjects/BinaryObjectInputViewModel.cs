using DiegoRangel.DotNet.Framework.CQRS.API.Mapper;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.IO;

namespace DiegoRangel.DotNet.Framework.CQRS.API.ViewModels.BinaryObjects
{
    public class BinaryObjectInputViewModel : IViewModel<BinaryObject>
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public long SizeInBytes { get; set; }
        public string TempFileName { get; set; }
        public string Base64Url { get; set; }
    }
}