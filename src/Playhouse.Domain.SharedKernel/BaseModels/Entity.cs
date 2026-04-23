namespace Playhouse.Domain.SharedKernel.BaseModels
{
    public abstract class Entity
    {
        public virtual int Id { get; protected set; }

        public bool IsTransient()
        {
            return Id == default;
        }

        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
            {
                return false;
            }

            if (IsTransient() || other.IsTransient())
            {
                return false;
            }
            else
            {
                return Id == other.Id;
            }
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                return (GetType().Name + Id).GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public static bool operator ==(Entity? left, Entity? right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.Equals(right);
        }

        public static bool operator !=(Entity? left, Entity? right)
        {
            return !(left == right);
        }
    }
}
