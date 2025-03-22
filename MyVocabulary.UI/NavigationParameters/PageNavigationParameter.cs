using MyVocabulary.UI.Enums;

namespace MyVocabulary.UI.NavigationParameters;

/// <summary>
/// Represents a navigation parameter that includes a <see cref="NavigationTypes"/> flag 
/// and a strongly typed value for passing between pages.
/// </summary>
/// <typeparam name="T">The type of the value being passed.</typeparam>
public class PageNavigationParameter<T> : NavigationParameterBase
    where T : class
{
    
    /// <summary>
    /// Gets the navigation mode indicating the context of the navigation.
    /// </summary>
    public NavigationTypes NavigationType => Get<NavigationTypes>(nameof(NavigationType));

    /// <summary>
    /// Gets the strongly typed value being passed as a navigation parameter.
    /// </summary>
    public T Value => Get<T>(nameof(Value));

    /// <summary>
    /// Initializes a new instance of the <see cref="PageNavigationParameter{T}"/> class 
    /// with a specified navigation mode and value.
    /// </summary>
    /// <param name="navigationType">The navigation mode.</param>
    /// <param name="value">The value to pass between pages.</param>
    public PageNavigationParameter(NavigationTypes navigationType, T value)
        : base((nameof(NavigationType), navigationType), (nameof(Value), value)) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PageNavigationParameter{T}"/> class 
    /// from an existing dictionary of query parameters.
    /// </summary>
    /// <param name="query">The dictionary containing navigation parameters.</param>
    private PageNavigationParameter(IDictionary<string, object> query) : base(query) { }

    /// <summary>
    /// Creates an instance of <see cref="PageNavigationParameter{T}"/> from a given query dictionary.
    /// </summary>
    /// <param name="query">The dictionary containing navigation parameters.</param>
    /// <returns>An instance of <see cref="PageNavigationParameter{T}"/> with the extracted values.</returns>
    public static PageNavigationParameter<T> From(IDictionary<string, object> query) 
        => new(query);
    
}
