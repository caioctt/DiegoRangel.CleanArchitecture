using System;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories.Segregations;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Analytics.Repositories
{
    public interface IResponseTrackingRepository : IFindableRepository<ResponseTracking, Guid>, ICreatableRepository<ResponseTracking, Guid>
    {
        
    }
}