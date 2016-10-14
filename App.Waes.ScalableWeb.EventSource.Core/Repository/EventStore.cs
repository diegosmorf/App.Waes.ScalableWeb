﻿using App.Cqrs.Core.Event;
using App.Waes.ScalableWeb.EventSource.Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.Waes.ScalableWeb.EventSource.Core.Repository
{
    public class EventStore : IEventStore
    {
        private readonly IEventPublisher eventPublisher;

        public struct EventDescriptor
        {
            public readonly IEvent EventData;
            public readonly Guid Id;
            public int Version;

            public EventDescriptor(Guid id, IEvent eventData, int version)
            {
                EventData = eventData;
                Version = version;
                Id = id;
            }
        }

        public EventStore(IEventPublisher publisher)
        {
            eventPublisher = publisher;
        }

        public readonly Dictionary<Guid, List<EventDescriptor>> Current = new Dictionary<Guid, List<EventDescriptor>>();

        public void SaveEvents(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion)
        {
            List<EventDescriptor> eventDescriptors;

            if (!Current.TryGetValue(aggregateId, out eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                Current.Add(aggregateId, eventDescriptors);
            }

            var i = expectedVersion;

            foreach (var @event in events)
            {
                i++;
                @event.Version = i;
                eventDescriptors.Add(new EventDescriptor(aggregateId, @event, i));
                eventPublisher.Publish(@event);
            }
        }

        public List<IEvent> GetEventsForAggregate(Guid aggregateId)
        {
            List<EventDescriptor> eventDescriptors;
            if (!Current.TryGetValue(aggregateId, out eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }

            return eventDescriptors.Select(desc => desc.EventData).ToList();
        }
    }
}