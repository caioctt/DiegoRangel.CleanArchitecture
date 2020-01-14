using System;
using MediatR;

namespace DiegoRangel.DotNet.Framework.CQRS.Infra.CrossCutting.MediatR
{
    public abstract class Event : INotification
    {
        public DateTime Timestamp { get; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}