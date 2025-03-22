namespace MyVocabulary.UI.NavigationParameters;

/// <summary>
/// Base class for navigation parameters used in Shell navigation.
/// Provides a structured way to pass and retrieve strongly typed parameters 
/// between pages using <see cref="ShellNavigationQueryParameters"/>.
/// </summary>
public abstract class NavigationParameterBase : ShellNavigationQueryParameters
{
    
    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationParameterBase"/> class 
    /// with the specified key-value parameters.
    /// </summary>
    /// <param name="parameters">An array of key-value pairs representing navigation parameters.</param>
    protected NavigationParameterBase(params (string Key, object Value)[] parameters)
    {
        foreach (var (key, value) in parameters)
        {
            Add(key, value);
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NavigationParameterBase"/> class 
    /// from an existing dictionary of query parameters.
    /// </summary>
    /// <param name="query">A dictionary containing navigation parameters.</param>
    protected NavigationParameterBase(IDictionary<string, object> query)
    {
        foreach (var (key, value) in query)
        {
            Add(key, value);
        }
    }

    /// <summary>
    /// Retrieves a strongly typed parameter value from the navigation query.
    /// Throws a <see cref="KeyNotFoundException"/> if the key is not found.
    /// </summary>
    /// <typeparam name="T">The expected type of the value.</typeparam>
    /// <param name="key">The key associated with the value.</param>
    /// <returns>The value of the specified type.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when the key does not exist in the query.</exception>
    public T Get<T>(string key)
    {
        if (TryGetValue(key, out var result))
        {
            return (T)result;
        }
        throw new KeyNotFoundException($"Key '{key}' not found.");
    }
    
}