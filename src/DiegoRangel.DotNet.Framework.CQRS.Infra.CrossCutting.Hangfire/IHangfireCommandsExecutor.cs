using System.Threading.Tasks;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Hangfire
{
    public interface IHangfireCommandsExecutor
    {
        Task ExecuteCommand(MediatorSerializedObject mediatorSerializedObject);
    }
}