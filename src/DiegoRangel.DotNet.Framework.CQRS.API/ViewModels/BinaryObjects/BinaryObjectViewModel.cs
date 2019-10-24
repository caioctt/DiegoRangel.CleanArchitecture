using DiegoRangel.DotNet.Framework.CQRS.API.AutoMapperSetup;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.IO;

namespace DiegoRangel.DotNet.Framework.CQRS.API.ViewModels.BinaryObjects
{
    public class BinaryObjectViewModel : IViewModelWithId<BinaryObject, long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public long SizeInBytes { get; set; }
    }
}