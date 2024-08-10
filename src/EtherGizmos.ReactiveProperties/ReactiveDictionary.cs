using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace EtherGizmos.ReactiveProperties;

/// <summary>
/// A reactive dictionary of values.
/// </summary>
/// <typeparam name="TKey">The key type.</typeparam>
/// <typeparam name="TValue">The value type.</typeparam>
public class ReactiveDictionary<TKey, TValue> : ReactiveProperty<IDictionary<TKey, TValue>>, IDictionary<TKey, TValue>
    where TKey : notnull
{
    /// <summary>
    /// Constructs the dictionary.
    /// </summary>
    internal ReactiveDictionary() : base(new Dictionary<TKey, TValue>()) { }

    /// <summary>
    /// Constructs the dictionary.
    /// </summary>
    /// <param name="value">The initial values.</param>
    internal ReactiveDictionary(IDictionary<TKey, TValue> value) : base(value.ToDictionary()) { }

    /// <inheritdoc/>
    public TValue this[TKey key]
    {
        get => Value[key];
        set
        {
            Value[key] = value;
            IsDirty = true;
        }
    }

    /// <inheritdoc/>
    public ICollection<TKey> Keys => Value.Keys;

    /// <inheritdoc/>
    public ICollection<TValue> Values => Value.Values;

    /// <inheritdoc/>
    public int Count => Value.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => Value.IsReadOnly;

    /// <inheritdoc/>
    public void Add(TKey key, TValue value)
    {
        Value.Add(key, value);
        IsDirty = true;
    }

    /// <inheritdoc/>
    public void Add(KeyValuePair<TKey, TValue> item)
    {
        Value.Add(item);
        IsDirty = true;
    }

    /// <inheritdoc/>
    public void Clear()
    {
        Value.Clear();
    }

    /// <inheritdoc/>
    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return Value.Contains(item);
    }

    /// <inheritdoc/>
    public bool ContainsKey(TKey key)
    {
        return Value.ContainsKey(key);
    }

    /// <inheritdoc/>
    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        Value.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc/>
    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return Value.GetEnumerator();
    }

    /// <inheritdoc/>
    public bool Remove(TKey key)
    {
        var result = Value.Remove(key);
        IsDirty = true;
        return result;
    }

    /// <inheritdoc/>
    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        var result = Value.Remove(item);
        IsDirty = true;
        return result;
    }

    /// <inheritdoc/>
    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        return Value.TryGetValue(key, out value);
    }

    /// <inheritdoc/>
    public void Replace(IDictionary<TKey, TValue> value)
    {
        Value = value.ToDictionary();
        IsDirty = true;
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return Value.GetEnumerator();
    }
}
