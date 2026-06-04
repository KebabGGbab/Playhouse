using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Playhouse.Core.Data.Comparers
{
    internal sealed class HashSetValueComparer<T> : ValueComparer<ISet<T>>
    {
        public HashSetValueComparer()
            : base(
                  (left, right) => EqualsCollections(left, right),
                  collection => GetHashCodeCollection(collection),
                  collection => SnapshotCollection(collection))
        {
        }

        public static bool EqualsCollections(ISet<T>? left, ISet<T>? right)
        {
            if (left is null ^ right is null)
            {
                return false;
            }

            return left is null || left.SequenceEqual(right!); /* right не может быть null во втором выражении */
        }

        public static int GetHashCodeCollection(ISet<T> collection)
        {
            return collection.Aggregate(0, (total, next) => HashCode.Combine(total, next?.GetHashCode()));
        }

        public static ISet<T> SnapshotCollection(ISet<T> collection)
        {
            return collection.ToHashSet();
        }
    }
}
