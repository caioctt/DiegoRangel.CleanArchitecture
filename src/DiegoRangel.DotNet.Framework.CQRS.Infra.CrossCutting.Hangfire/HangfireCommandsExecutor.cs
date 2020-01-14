using System.Linq;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;
using MediatR;
using Newtonsoft.Json;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Hangfire
{
    public class HangfireCommandsExecutor : IHangfireCommandsExecutor
    {
        private readonly IMediator _mediator;
        private readonly MediatrAssemblies _mediatrAssemblies;

        public HangfireCommandsExecutor(IMediator mediator, MediatrAssemblies mediatrAssemblies)
        {
            _mediator = mediator;
            _mediatrAssemblies = mediatrAssemblies;
        }

        public Task ExecuteCommand(MediatorSerializedObject mediatorSerializedObject)
        {
            var types = _mediatrAssemblies.Assemblies.SelectMany(x => x.GetTypes()).ToList();
            var type = types.FirstOrDefault(x =>
                x.IsClass 
                && !x.IsAbstract
                && !string.IsNullOrEmpty(x.FullName)
                && x.FullName.Equals(mediatorSerializedObject.FullTypeName));
            
            if (type == null) return null;
            dynamic req = JsonConvert.DeserializeObject(mediatorSerializedObject.Data, type);
            return _mediator.Send(req);
        }
    }
}