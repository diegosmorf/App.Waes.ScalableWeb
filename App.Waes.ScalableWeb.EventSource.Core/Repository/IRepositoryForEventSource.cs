using App.Waes.ScalableWeb.EventSource.Core.Domain;
using System;

namespace App.Waes.ScalableWeb.EventSource.Core.Repository
{
    public interface IRepositoryForEventSource<out TAggregate>
        where TAggregate : AggregateRootForEventSource, new()
    {
        void Save(AggregateRootForEventSource aggregate, int expectedVersion);

        TAggregate GetById(Guid id);
    }
}