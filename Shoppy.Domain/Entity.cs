using Shoppy.Domain.Abstractions;

namespace Shoppy.Domain
{
    public abstract class Entity<TKey> : IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public virtual TKey Id { get; protected set; } = default!;

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            if (obj is not Entity<TKey> entity) return false;
            if (GetUnproxiedType(this) != GetUnproxiedType(obj)) return false;
            if (Id.Equals(default) || entity.Id.Equals(default))return false;
            return Id.Equals(entity.Id);
        }

        public Type GetUnproxiedType(object obj)
        {
            const string EFCoreProxyPrefix = "Castle.Proxies.";
            const string NHibernateProxyPostfix = "Proxy";

            Type type = obj.GetType();
            string typeString = type.ToString();

            if (typeString.Contains(EFCoreProxyPrefix) || typeString.EndsWith(NHibernateProxyPostfix))
                return type.BaseType;

            return type;
        }

        public override int GetHashCode()
        {
            return (GetUnproxiedType(this).ToString() + Id).GetHashCode();
        }
    }
}