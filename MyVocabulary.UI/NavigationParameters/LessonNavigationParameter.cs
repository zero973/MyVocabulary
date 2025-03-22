using MyVocabulary.Application.Models;

namespace MyVocabulary.UI.NavigationParameters;

public class LessonNavigationParameter : NavigationParameterBase
{

    public List<PhraseUsageDTO> OriginalPhrases => Get<List<PhraseUsageDTO>>(nameof(OriginalPhrases));

    public List<PhraseUsageDTO> ReversedPhrases => Get<List<PhraseUsageDTO>>(nameof(ReversedPhrases));

    public LessonNavigationParameter(List<PhraseUsageDTO> originalPhrases, List<PhraseUsageDTO> reversedPhrases)
        : base((nameof(OriginalPhrases), originalPhrases), 
            (nameof(ReversedPhrases), reversedPhrases)) { }

    public static LessonNavigationParameter From(IDictionary<string, object> dictionary)
        => new(
            (List<PhraseUsageDTO>)dictionary[nameof(OriginalPhrases)], 
            (List<PhraseUsageDTO>)dictionary[nameof(ReversedPhrases)]
        );
    
}