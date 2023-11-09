using System.Reflection;

namespace Car_Rental.Common.Extensions;

public static class CollectionExtensions
{
    public static FieldInfo? FindCollection<T>(this FieldInfo[] fields) where T : class
        => fields.FirstOrDefault(f => f.FieldType == typeof(List<T>) && f.IsInitOnly)
        ?? throw new InvalidOperationException("Unsupported type");

    public static List<T>? GetData<T>(this FieldInfo? field, object obj)
        => (List<T>?)field?.GetValue(obj)
        ?? throw new InvalidDataException();

    public static IQueryable<T> ToQueryable<T>(this List<T>? data)
        => data is not null && data is List<T>
        ? data.AsQueryable()
        : throw new InvalidDataException();

    public static List<T> Filter<T>(this IQueryable<T> collection, Func<T, bool>? lambda)
        => lambda is null ? collection.ToList() : collection.Where(lambda).ToList();
}