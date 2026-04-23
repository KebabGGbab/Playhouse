namespace Playhouse.Domain.SharedKernel.BaseModels
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.Equals(right);
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !(left == right);
        }
    }
}
