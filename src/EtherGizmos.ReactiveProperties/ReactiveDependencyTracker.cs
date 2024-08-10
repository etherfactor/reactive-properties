namespace EtherGizmos.ReactiveProperties;

/// <summary>
/// Provides thread-specific tracking of <see cref="ReactiveProperty{TValue}"/> dependencies.
/// </summary>
internal static class ReactiveDependencyTracker
{
    private static ThreadLocal<Stack<HashSet<ReactiveProperty>>> _dependencyStack = new();

    /// <summary>
    /// Begin tracking a property evaluation.
    /// </summary>
    public static void StartTracking()
    {
        var stack = _dependencyStack.Value ??= new();
        stack.Push([]);
    }

    /// <summary>
    /// End tracking a property evaluation.
    /// </summary>
    public static void StopTracking()
    {
        var stack = _dependencyStack.Value ??= new();
        var subDependencies = stack.Pop();

        //If there is a prior stack, add the inner dependencies to the outer dependencies
        if (stack.Count > 0)
        {
            var outerDependencies = stack.Peek();
            foreach (var dep in outerDependencies)
            {
                outerDependencies.Add(dep);
            }
        }
    }

    /// <summary>
    /// Reports a property as a dependency of the current context.
    /// </summary>
    /// <param name="dependency">The property to report.</param>
    public static void ReportDependency(ReactiveProperty dependency)
    {
        var stack = _dependencyStack.Value;
        if (stack is not null && stack.Count > 0)
        {
            var dependencies = stack.Peek();
            dependencies.Add(dependency);
        }
    }

    /// <summary>
    /// Gets the current context's dependencies.
    /// </summary>
    /// <returns>The set of dependencies.</returns>
    public static IEnumerable<ReactiveProperty> GetDependencies()
    {
        var stack = _dependencyStack.Value ??= new();
        var dependencies = stack.Peek();
        return dependencies.AsEnumerable();
    }
}
