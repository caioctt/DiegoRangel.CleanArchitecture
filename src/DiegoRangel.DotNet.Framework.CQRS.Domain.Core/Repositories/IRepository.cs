using System;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Repositories
{
    /// <summary>
    /// An interface used only to automatically add it's implementations on DI's scope.
    /// </summary>
    public interface IRepository : IDisposable
    {
        
    }
}