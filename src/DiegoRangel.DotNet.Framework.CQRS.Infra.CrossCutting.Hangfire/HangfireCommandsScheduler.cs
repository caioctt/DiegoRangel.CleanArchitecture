using System;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;
using Hangfire;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Hangfire
{
    public class HangfireCommandsScheduler : IHangfireCommandsScheduler
    {
        private readonly IHangfireCommandsExecutor _commandsExecutor;

        public HangfireCommandsScheduler(IHangfireCommandsExecutor commandsExecutor)
        {
            _commandsExecutor = commandsExecutor;
        }

        public string SendNow(ICommand request)
        {
            var mediatorSerializedObject = SerializeObject(request);

            return BackgroundJob.Enqueue(() => _commandsExecutor.ExecuteCommand(mediatorSerializedObject));
        }
        public void Schedule(ICommand request, DateTimeOffset scheduleAt)
        {
            var mediatorSerializedObject = SerializeObject(request);

            BackgroundJob.Schedule(() => _commandsExecutor.ExecuteCommand(mediatorSerializedObject), scheduleAt);
        }
        public void Schedule(ICommand request, TimeSpan delay)
        {
            var mediatorSerializedObject = SerializeObject(request);
            var newTime = DateTime.Now + delay;
            BackgroundJob.Schedule(() => _commandsExecutor.ExecuteCommand(mediatorSerializedObject), newTime);
        }
        public void ScheduleRecurring(ICommand request, string cronExpression)
        {
            var mediatorSerializedObject = SerializeObject(request);
            RecurringJob.AddOrUpdate(request.GetType().Name, () => _commandsExecutor.ExecuteCommand(mediatorSerializedObject), cronExpression);
        }

        private static MediatorSerializedObject SerializeObject(object mediatorObject)
        {
            var fullTypeName = mediatorObject.GetType().FullName;
            var data = JsonConvert.SerializeObject(mediatorObject, new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ContractResolver = new DefaultContractResolver()
            });

            return new MediatorSerializedObject(fullTypeName, data, null);
        }
    }
}