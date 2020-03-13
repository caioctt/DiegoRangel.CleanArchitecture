using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.Context;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.MongoDB.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;
        public UnitOfWork(IMongoContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}