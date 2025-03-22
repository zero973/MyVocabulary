using MyVocabulary.Application.Models;
using MyVocabulary.UI.Enums;

namespace MyVocabulary.UI.NavigationParameters;

/// <summary>
/// Represents a navigation parameter for passing data related to phrase usage, 
/// including a navigation mode, topic and phrase usage details.
/// </summary>
public class PhraseUsageNavigationParameter : NavigationParameterBase
{
    
    /// <summary>
    /// Gets the navigation mode indicating the context of the navigation.
    /// </summary>
    public NavigationTypes NavigationType => Get<NavigationTypes>(nameof(NavigationType));

    /// <summary>
    /// Gets the topic associated with the phrase usage.
    /// </summary>
    public TopicDTO Topic => Get<TopicDTO>(nameof(Topic));

    /// <summary>
    /// Gets the phrase usage details being passed.
    /// </summary>
    public PhraseUsageDTO PhraseUsage => Get<PhraseUsageDTO>(nameof(PhraseUsage));

    /// <summary>
    /// Initializes a new instance of the <see cref="PhraseUsageNavigationParameter"/> class
    /// with the specified navigation mode, topic, and optional phrase usage details.
    /// </summary>
    /// <param name="navigationType">The navigation mode.</param>
    /// <param name="topic">The topic associated with the phrase usage.</param>
    /// <param name="phraseUsage">The phrase usage details</param>
    public PhraseUsageNavigationParameter(NavigationTypes navigationType, TopicDTO topic, PhraseUsageDTO phraseUsage)
        : base((nameof(NavigationType), navigationType), 
               (nameof(Topic), topic), 
               (nameof(PhraseUsage), phraseUsage)) { }

    /// <summary>
    /// Creates an instance of <see cref="PhraseUsageNavigationParameter"/> from a given query dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary containing navigation parameters.</param>
    /// <returns>An instance of <see cref="PhraseUsageNavigationParameter"/> with the extracted values.</returns>
    public static PhraseUsageNavigationParameter From(IDictionary<string, object> dictionary)
        => new(
            (NavigationTypes)dictionary[nameof(NavigationType)], 
            (TopicDTO)dictionary[nameof(Topic)], 
            (PhraseUsageDTO)dictionary[nameof(PhraseUsage)]
        );
    
}