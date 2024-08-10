using System.Collections;

namespace EtherGizmos.ReactiveProperties;

/// <summary>
/// A reactive list of values.
/// </summary>
/// <typeparam name="TValue">The value type.</typeparam>
public class ReactiveList<TValue> : ReactiveProperty<IList<TValue>>, IList<TValue>
{
    /// <summary>
    /// Constructs the list.
    /// </summary>
    internal ReactiveList() : base([]) { }

    /// <summary>
    /// Constructs the list.
    /// </summary>
    /// <param name="value">The initial values.</param>
    public ReactiveList(IList<TValue> value) : base([.. value]) { }

    /// <inheritdoc/>
    public TValue this[int index]
    {
        get => Value[index];
        set
        {
            Value[index] = value;
            IsDirty = true;
        }
    }

    /// <inheritdoc/>
    public int Count => Value.Count;

    /// <inheritdoc/>
    public bool IsReadOnly => Value.IsReadOnly;

    /// <inheritdoc/>
    public void Add(TValue item)
    {
        Value.Add(item);
        IsDirty = true;
    }

    /// <inheritdoc/>
    public void Clear()
    {
        Value.Clear();
        IsDirty = true;
    }

    /// <inheritdoc/>
    public bool Contains(TValue item)
    {
        return Value.Contains(item);
    }

    /// <inheritdoc/>
    public void CopyTo(TValue[] array, int arrayIndex)
    {
        Value.CopyTo(array, arrayIndex);
    }

    /// <inheritdoc/>
    public IEnumerator<TValue> GetEnumerator()
    {
        return Value.GetEnumerator();
    }

    /// <inheritdoc/>
    public int IndexOf(TValue item)
    {
        return Value.IndexOf(item);
    }

    /// <inheritdoc/>
    public void Insert(int index, TValue item)
    {
        Value.Insert(index, item);
        IsDirty = true;
    }

    /// <inheritdoc/>
    public bool Remove(TValue item)
    {
        var result = Value.Remove(item);
        IsDirty = true;
        return result;
    }

    /// <inheritdoc/>
    public void RemoveAt(int index)
    {
        Value.RemoveAt(index);
        IsDirty = true;
    }

    /// <inheritdoc/>
    public void Replace(IList<TValue> value)
    {
        Value = [.. value];
        IsDirty = true;
    }

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return Value.GetEnumerator();
    }
}
