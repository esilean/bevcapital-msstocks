using BevCapital.Stocks.Domain.Core.Events;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BevCapital.Stocks.Domain.Core.EventStores.Aggregate
{
    public abstract class Aggregate : IAggregate
    {
        public string Id { get; protected set; }
        public int Version { get; protected set; } = 0;
        public DateTime CreatedAtUtc { get; protected set; }
        public DateTime UpdatedAtUtc { get; protected set; }

        [NonSerialized]
        private readonly List<IEvent> uncommittedEvents = new List<IEvent>();

        public bool Valid { get; private set; }
        public bool Invalid => !Valid;
        public ValidationResult ValidationResult { get; private set; }

        [System.ComponentModel.DataAnnotations.Timestamp]
        public byte[] RowVersion { get; private set; }

        public bool Validate<TModel>(TModel model, AbstractValidator<TModel> validator)
        {
            ValidationResult = validator.Validate(model);
            return Valid = ValidationResult.IsValid;
        }

        protected Aggregate()
        { }

        IEnumerable<IEvent> IAggregate.DequeueUncommittedEvents()
        {
            var dequeuedEvents = uncommittedEvents.ToList();

            uncommittedEvents.Clear();

            return dequeuedEvents;
        }

        protected virtual void Enqueue(IEvent @event)
        {
            Version++;
            CreatedAtUtc = DateTime.UtcNow;
            uncommittedEvents.Add(@event);
        }
    }
}
