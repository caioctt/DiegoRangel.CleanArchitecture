using System.Threading.Tasks;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}