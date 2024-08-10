namespace EtherGizmos.ReactiveProperties;

/// <summary>
/// A reactive property.
/// </summary>
public abstract class ReactiveProperty : IMutable
{
    private long _isDirtyTime = DateTimeOffset.MinValue.Ticks + 1;
    private long _isRecalculatedTime = DateTimeOffset.MinValue.Ticks;
    private readonly List<ReactiveProperty> _dependencies = [];

    /// <inheritdoc/>
    public virtual bool IsDirty
    {
        get
        {
            var isDirty = IsDirtyTime > IsRecalculatedTime || Dependencies.Any(e => e.IsDirtyTime > IsRecalculatedTime);

            //If this property is dirty, but it has not been modified, flag it as dirty. Not required, but more intuitive
            //when inspecting properties while debugging
            if (isDirty && IsDirtyTime <= IsRecalculatedTime)
                IsDirty = true;

            return isDirty;
        }
        set
        {
            if (value)
            {
                //If dirty, dirty time is ahead of recalculated time
                IsDirtyTime = DateTimeOffset.UtcNow;
            }
            else
            {
                //If not dirty, recalculated time is ahead of dirty time
                IsRecalculatedTime = DateTimeOffset.UtcNow;
            }
        }
    }

    /// <inheritdoc/>
    public virtual DateTimeOffset IsDirtyTime
    {
        get => new(_isDirtyTime, TimeSpan.Zero);
        set => _isDirtyTime = value.Ticks;
    }

    /// <inheritdoc/>
    public virtual DateTimeOffset IsRecalculatedTime
    {
        get => new(_isRecalculatedTime, TimeSpan.Zero);
        set => _isRecalculatedTime = value.Ticks;
    }

    /// <summary>
    /// The list of properties on which the current property depends.
    /// </summary>
    protected List<ReactiveProperty> Dependencies => _dependencies;
}

/// <summary>
/// A reactive property.
/// </summary>
/// <typeparam name="TValue">The value type.</typeparam>
public class ReactiveProperty<TValue> : ReactiveProperty
{
    protected TValue _value = default!;
    private Func<TValue> _evaluator;

    /// <summary>
    /// The property value. Will be recalculated if dirty.
    /// </summary>
    public virtual TValue Value
    {
        get
        {
            if (IsDirty)
            {
                Recalculate();
            }

            //If anything is tracking this as a dependency, report it
            ReactiveDependencyTracker.ReportDependency(this);

            return _value;
        }

        set
        {
            _evaluator = () => value;
            IsDirty = true;
        }
    }

    /// <summary>
    /// Construct the property.
    /// </summary>
    /// <param name="value">The initial value.</param>
    internal protected ReactiveProperty(TValue value)
    {
        _evaluator = () => value;
        IsDirty = true;
    }

    /// <summary>
    /// Construct the property.
    /// </summary>
    /// <param name="evaluator">The evaluator to calculate a value.</param>
    internal protected ReactiveProperty(Func<TValue> evaluator)
    {
        _evaluator = evaluator;
        IsDirty = true;
    }

    /// <summary>
    /// Recalculate the property.
    /// </summary>
    private void Recalculate()
    {
        TrackDependencies(() =>
        {
            _value = _evaluator();
        });
        IsDirty = false;
    }

    /// <summary>
    /// Recalculate dependencies for a context.
    /// </summary>
    /// <param name="action">The evaluation context.</param>
    private void TrackDependencies(Action action)
    {
        //Begin tracking dependencies
        ReactiveDependencyTracker.StartTracking();

        //Run the contextual action
        action();

        //Import the calculated dependencies
        Dependencies.Clear();
        Dependencies.AddRange(ReactiveDependencyTracker.GetDependencies());

        //Stop tracking dependencies
        ReactiveDependencyTracker.StopTracking();
    }
}
