using MediatR;

namespace Shoppy.Domain
{
    public abstract class DomainEvent : INotification
    {
        public DateTimeOffset OccuredOn { get; protected set; }
    }
}