using System.Collections.Generic;
using System.Threading.Tasks;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Entities;
using DiegoRangel.DotNet.Framework.CQRS.Domain.Core.Interfaces;
using Stateless;
using Stateless.Reflection;

namespace DiegoRangel.DotNet.Framework.CQRS.Domain.Core.StateMachines
{
    public abstract class StateMachineBase<TEntity, TKey, TStates, TTriggers> : IStateMachine
        where TEntity : IEntity<TKey>
        where TStates : struct
        where TTriggers : struct
    {
        protected StateMachineBase()
        {
            CustomTriggers = new Dictionary<TTriggers, StateMachine<TStates, TTriggers>.TriggerWithParameters>();

            Machine = new StateMachine<TStates, TTriggers>(() => CurrentState, s => CurrentState = s);
            Machine.OnTransitioned(OnTransitioned);
            Machine.OnUnhandledTrigger((state, trigger) =>
            {
                Entity.AddValidationError("Status", "Operação inválida.");
            });

            Setup();
        }

        protected TEntity Entity { get; set; }
        protected TStates CurrentState { get; set; }
        protected StateMachine<TStates, TTriggers> Machine { get; set; }
        protected Dictionary<TTriggers, StateMachine<TStates, TTriggers>.TriggerWithParameters> CustomTriggers { get; set; }

        protected abstract Task OnBeforeFire();
        protected abstract void Setup();
        protected abstract void OnTransitioned(StateMachine<TStates, TTriggers>.Transition transition);

        public async Task Fire(TEntity entity, TStates currentState, TTriggers trigger)
        {
            Entity = entity;
            CurrentState = currentState;

            await OnBeforeFire();
            await Machine.FireAsync(trigger);
        }
        public async Task FireWithParameter<T>(TEntity entity, TStates currentState, TTriggers trigger, T value)
        {
            Entity = entity;
            CurrentState = currentState;

            await OnBeforeFire();
            await Machine.FireAsync(TriggerWithParameter<T>(trigger), value);
        }

        public StateMachineInfo Graph()
        {
            return Machine.GetInfo();
        }

        protected StateMachine<TStates, TTriggers>.TriggerWithParameters<T> TriggerWithParameter<T>(TTriggers trigger)
        {
            if (CustomTriggers.ContainsKey(trigger))
                return (StateMachine<TStates, TTriggers>.TriggerWithParameters<T>)CustomTriggers[trigger];

            var newCustomTrigger = Machine.SetTriggerParameters<T>(trigger);
            CustomTriggers.Add(trigger, newCustomTrigger);

            return newCustomTrigger;
        }
    }

    public abstract class StateMachineBase<TEntity, TStates, TTriggers> : 
        StateMachineBase<TEntity, int, TStates, TTriggers>
        where TEntity : IEntity
        where TStates : struct
        where TTriggers : struct
    {}
}