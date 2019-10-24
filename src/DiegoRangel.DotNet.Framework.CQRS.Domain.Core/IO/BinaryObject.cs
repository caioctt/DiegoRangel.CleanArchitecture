using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.IO
{
    public class BinaryObject : Entity<long>
    {
        public string Name { get; set; }
        public string Extension { get; set; }
        public string ContentType { get; set; }
        public long SizeInBytes { get; set; }
        public byte[] Stream { get; set; }
    }
}