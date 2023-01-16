namespace Shoppy.Domain.Abstractions
{
    public interface IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; }
    }
}
