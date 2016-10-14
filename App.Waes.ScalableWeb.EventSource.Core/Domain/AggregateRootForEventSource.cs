﻿using App.Cqrs.Core.Event;
using App.Waes.ScalableWeb.EventSource.Core.Extension;
using System;
using System.Collections.Generic;

namespace App.Waes.ScalableWeb.EventSource.Core.Domain
{
    public abstract class AggregateRootForEventSource : IAggregateRootForEventSource
    {
        private readonly List<IEvent> appliedEvents = new List<IEvent>();

        public Guid Id { get; protected set; }
        public int Version { get; protected set; }

        public IEnumerable<IEvent> AppliedEvents => appliedEvents;

        protected void OnApplied(IEvent @event)
        {
            appliedEvents.Add(@event);
            Version++;
        }

        public void MarkChangesAsCommitted()
        {
            appliedEvents.Clear();
        }

        public void LoadsFromHistory(IEnumerable<IEvent> historyEvents)
        {
            foreach (var @event in historyEvents)
                ApplyChange(@event, false);
        }

        private void ApplyChange(IEvent @event, bool isNew)
        {
            this.AsDynamic().ApplyChange(@event);
            if (isNew)
                appliedEvents.Add(@event);
        }
    }
}