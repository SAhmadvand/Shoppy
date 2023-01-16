namespace Shoppy.Domain.Abstractions
{
    internal interface IAggregateRoot
    {
        IReadOnlyList<DomainEvent> Events { get; }
    }
}
