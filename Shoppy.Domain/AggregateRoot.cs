using Shoppy.Domain.Abstractions;

namespace Shoppy.Domain
{
    public abstract class AggregateRoot<TKey> : IEntity<TKey>, IAggregateRoot
        where TKey : IEquatable<TKey>
    {
        protected AggregateRoot()
        {
        }

        protected AggregateRoot(TKey id)
        {
            if (id is null) throw new ArgumentNullException(nameof(id));
            if (id.Equals(default)) throw new ArgumentException("invalid value", nameof(id));

            Id = id;
        }

        public TKey Id { get; protected set; } = default!;


        private readonly List<DomainEvent> _events = new();
        public IReadOnlyList<DomainEvent> Events => _events.AsReadOnly();

        protected void RaiseEvent(DomainEvent @event)
        {
            if (!_events.Contains(@event))
                _events.Add(@event);
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            if (obj is not AggregateRoot<TKey> entity) return false;

            return Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(AggregateRoot<TKey> left, AggregateRoot<TKey> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(AggregateRoot<TKey> left, AggregateRoot<TKey> right)
        {
            return !(left == right);
        }
    }
}