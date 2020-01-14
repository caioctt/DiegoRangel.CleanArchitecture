using System;
using DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.Hangfire
{
    public interface IHangfireCommandsScheduler
    {
        string SendNow(ICommand request);
        void Schedule(ICommand request, DateTimeOffset scheduleAt);
        void Schedule(ICommand request, TimeSpan delay);
        void ScheduleRecurring(ICommand request, string cronExpression);
    }
}