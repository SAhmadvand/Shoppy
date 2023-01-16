namespace Shoppy.Domain
{
    public abstract class ValueObject : IEquatable<ValueObject>
    {
        protected abstract IEnumerable<object> GetEquality();

        public bool Equals(ValueObject? other)
        {
            return other is not null && GetEquality().SequenceEqual(other.GetEquality());
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is not ValueObject) return false;
            return Equals((ValueObject)obj);
        }

        public override int GetHashCode()
        {
            return GetEquality()
                .Aggregate(1, (current, obj) =>
                {
                    unchecked
                    {
                        return current * 23 + (obj?.GetHashCode() ?? 0);
                    }
                });
        }

        public static bool operator ==(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
                return true;

            if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(right == left);
        }
    }
}
