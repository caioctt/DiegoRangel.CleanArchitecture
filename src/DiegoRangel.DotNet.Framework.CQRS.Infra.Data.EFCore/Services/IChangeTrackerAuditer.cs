using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services
{
    public interface IChangeTrackerAuditer
    {
        void Audit(ChangeTracker changeTracker);
    }
}