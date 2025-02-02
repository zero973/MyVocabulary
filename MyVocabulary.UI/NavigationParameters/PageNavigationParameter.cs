using MyVocabulary.UI.Enums;

namespace MyVocabulary.UI.NavigationParameters;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageNavigationParameter<T> : ShellNavigationQueryParameters
    where T : class
{

    /// <summary>
    /// 
    /// </summary>
    public NavigationModes Mode 
    {
        get 
        {
            TryGetValue(nameof(Mode), out var result);
            return (NavigationModes)Convert.ToInt32(result!.ToString());
        } 
    }

    /// <summary>
    /// 
    /// </summary>
    public T? Value
    {
        get
        {
            TryGetValue(nameof(Value), out var result);
            return result as T;
        }
    }

    public PageNavigationParameter(NavigationModes mode, T? value = null)
    {
        Add(nameof(Mode), (int)mode);
        if (value != null)
            Add(nameof(Value), value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static PageNavigationParameter<T> From(IDictionary<string, object> dictionary)
    {
        if (dictionary == null) throw new ArgumentNullException("Empty dictionary");
        if (!dictionary.ContainsKey(nameof(Mode))) throw new ArgumentNullException("Missed mode setting");

        var mode = (NavigationModes)Convert.ToInt32(dictionary[nameof(Mode)].ToString());
        if (mode == NavigationModes.New)
            return new PageNavigationParameter<T>(mode, null);

        var value = dictionary[nameof(Value)] as T;
        if (mode == NavigationModes.Exists && value == null)
            throw new ArgumentNullException("Value can't be null with this mode setting");

        return new PageNavigationParameter<T>(mode, value);
    }

}