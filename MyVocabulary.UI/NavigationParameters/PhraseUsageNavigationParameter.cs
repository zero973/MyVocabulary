using MyVocabulary.Application.Models;
using MyVocabulary.UI.Enums;

namespace MyVocabulary.UI.NavigationParameters;

public class PhraseUsageNavigationParameter : ShellNavigationQueryParameters
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
    public TopicDTO Topic
    {
        get
        {
            TryGetValue(nameof(Topic), out var result);
            return (result as TopicDTO)!;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public PhraseUsageDTO? Value
    {
        get
        {
            TryGetValue(nameof(Value), out var result);
            return result as PhraseUsageDTO;
        }
    }

    public PhraseUsageNavigationParameter(NavigationModes mode, TopicDTO topic, PhraseUsageDTO? value = null)
    {
        Add(nameof(Mode), (int)mode);
        Add(nameof(Topic), topic);
        if (value != null)
            Add(nameof(Value), value);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dictionary"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static PhraseUsageNavigationParameter From(IDictionary<string, object> dictionary)
    {
        if (dictionary == null) throw new ArgumentNullException("Empty dictionary");
        if (!dictionary.ContainsKey(nameof(Mode))) throw new ArgumentNullException("Missed mode setting");
        if (!dictionary.ContainsKey(nameof(Topic))) throw new ArgumentNullException("Missed topic setting");

        var mode = (NavigationModes)Convert.ToInt32(dictionary[nameof(Mode)].ToString());
        var topic = dictionary[nameof(Topic)] as TopicDTO;
        if (mode == NavigationModes.New)
            return new PhraseUsageNavigationParameter(mode, topic!);

        var value = dictionary[nameof(Value)] as PhraseUsageDTO;
        if (mode == NavigationModes.Exists && value == null)
            throw new ArgumentNullException("Value can't be null with this mode setting");

        return new PhraseUsageNavigationParameter(mode, topic!, value);
    }

}