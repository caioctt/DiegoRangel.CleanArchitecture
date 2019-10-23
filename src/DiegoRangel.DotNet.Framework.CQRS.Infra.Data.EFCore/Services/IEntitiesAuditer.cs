using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services
{
    public interface IEntitiesAuditer
    {
        void Analise(ChangeTracker changeTracker);
    }
}