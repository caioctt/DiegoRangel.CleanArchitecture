using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.Data.EFCore.Services
{
    public interface IChangeTrackerAuditer
    {
        Task Audit(ChangeTracker changeTracker);
    }
}