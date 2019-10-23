using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services;
using Microsoft.EntityFrameworkCore;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;
        private readonly IEntitiesAuditer _entitiesAuditer;

        public UnitOfWork(DbContext context, IEntitiesAuditer entitiesAuditer)
        {
            _context = context;
            _entitiesAuditer = entitiesAuditer;
        }

        public async Task<bool> Commit()
        {
            _entitiesAuditer.Analise(_context.ChangeTracker);

            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}