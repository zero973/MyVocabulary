namespace MyVocabulary.UI.NavigationParameters;

/// <summary>
/// Represents a navigation parameter containing a single strongly typed value 
/// for passing between pages without a navigation mode.
/// </summary>
/// <typeparam name="T">The type of the value being passed.</typeparam>
public class SimpleNavigationParameter<T> : NavigationParameterBase
    where T : class
{
    
    /// <summary>
    /// Gets the strongly typed value being passed as a navigation parameter.
    /// </summary>
    public T Value => Get<T>(nameof(Value));

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleNavigationParameter{T}"/> class 
    /// with the specified value.
    /// </summary>
    /// <param name="value">The value to pass between pages.</param>
    public SimpleNavigationParameter(T value) 
        : base((nameof(Value), value)) { }

    /// <summary>
    /// Creates an instance of <see cref="SimpleNavigationParameter{T}"/> from a given query dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary containing navigation parameters.</param>
    /// <returns>An instance of <see cref="SimpleNavigationParameter{T}"/> with the extracted value.</returns>
    public static SimpleNavigationParameter<T> From(IDictionary<string, object> dictionary)
        => new((T)dictionary[nameof(Value)]);
    
}