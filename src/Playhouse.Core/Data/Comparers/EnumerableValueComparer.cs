using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Playhouse.Core.Data.Comparers
{
    internal sealed class EnumerableValueComparer<T> : ValueComparer<IEnumerable<T>>
    {
        public EnumerableValueComparer()
            : base(
                  (left, right) => EqualsCollections(left, right),
                  collection => GetHashCodeCollection(collection),
                  collection => SnapshotCollection(collection))
        {
        }

        public static bool EqualsCollections(IEnumerable<T>? left, IEnumerable<T>? right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.SequenceEqual(right!); /* right не может быть null во втором выражении */
        }

        public static int GetHashCodeCollection(IEnumerable<T> collection)
        {
            return collection.Aggregate(0, (total, next) => HashCode.Combine(total, next?.GetHashCode()));
        }

        public static IEnumerable<T> SnapshotCollection(IEnumerable<T> collection)
        {
            return collection.ToList();
        }
    }
}
