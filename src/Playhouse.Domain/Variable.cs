namespace Playhouse.Domain
{
    public sealed class Variable
    {
        private static readonly Predicate<string?> _changeNamePredicate = (n) => !string.IsNullOrWhiteSpace(n);
        private static readonly Predicate<string?> _changeValuePredicate = (v) => v != null;

        public string Name 
        {
            get; 
            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace(value);

                field = value;
            }
        }

        public string Value 
        {
            get;
            set
            {
                ArgumentNullException.ThrowIfNull(value);

                field = value;
            } 
        }

        public Variable(string name, string value = "")
        {
            Name = name;
            Value = value;
        }

        public static bool CanCreate(string name, string value)
        {
            return _changeNamePredicate(name) && _changeValuePredicate(value);
        }

        public static bool ValidName(string newName)
        {
            return _changeNamePredicate(newName);
        }

        public static bool ValidValue(string newValue)
        {
            return _changeValuePredicate(newValue);
        }

        private IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Value;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj.GetType() != GetType())
            {
                return false;
            }

            Variable other = (Variable)obj;

            return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return GetEqualityComponents()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }

        public static bool operator ==(Variable? left, Variable? right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.Equals(right);
        }

        public static bool operator !=(Variable? left, Variable? right)
        {
            return !(left == right);
        }
    }
}
