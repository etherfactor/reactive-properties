namespace EtherGizmos.ReactiveProperties;

/// <summary>
/// Creates reactive properties.
/// </summary>
public static class Rx
{
    /// <summary>
    /// Creates a reactive simple property.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <param name="value">The initial value.</param>
    /// <returns>The reactive simple property.</returns>
    public static ReactiveProperty<TValue> Of<TValue>(TValue value)
    {
        return new(value);
    }

    /// <summary>
    /// Creates a reactive list property.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <returns>The reactive list property.</returns>
    public static ReactiveList<TValue> List<TValue>()
    {
        return new();
    }

    /// <summary>
    /// Creates a reactive list property.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <param name="list">The initial values.</param>
    /// <returns>The reactive list property.</returns>
    public static ReactiveList<TValue> List<TValue>(IList<TValue> list)
    {
        return new(list);
    }

    /// <summary>
    /// Creates a reactive dictionary property.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <returns>The reactive dictionary property.</returns>
    public static ReactiveDictionary<TKey, TValue> Dict<TKey, TValue>()
        where TKey : notnull
    {
        return new();
    }

    /// <summary>
    /// Creates a reactive dictionary property.
    /// </summary>
    /// <typeparam name="TKey">The key type.</typeparam>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <param name="dictionary">The initial values.</param>
    /// <returns>The reactive dictionary property.</returns>
    public static ReactiveDictionary<TKey, TValue> Dict<TKey, TValue>(IDictionary<TKey, TValue> dictionary)
        where TKey : notnull
    {
        return new(dictionary);
    }

    /// <summary>
    /// Creates a computed reactive property.
    /// </summary>
    /// <typeparam name="TValue">The value type.</typeparam>
    /// <param name="evaluator">The evaluator to compute the value.</param>
    /// <returns>The computed reactive property.</returns>
    public static ReactiveProperty<TValue> Eval<TValue>(Func<TValue> evaluator)
    {
        return new(evaluator);
    }
}
