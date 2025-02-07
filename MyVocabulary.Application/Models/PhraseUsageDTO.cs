namespace MyVocabulary.Application.Models;

public class PhraseUsageDTO
{

    public Guid Id { get; set; }

    public TopicDTO Topic { get; set; }

    public PhraseDTO NativePhrase { get; set; }

    public PhraseDTO TranslationPhrase { get; set; }

    public string NativeSentence { get; set; }

    public string TranslatedSentence { get; set; }

    public string? PhotoUrl { get; set; }

    public PhraseUsageDTO(Guid id, TopicDTO topic, PhraseDTO nativePhrase,
        PhraseDTO translationPhrase, string nativeSentence, 
        string translatedSentence, string? photoUrl)
    {
        Id = id;
        Topic = topic;
        NativePhrase = nativePhrase;
        TranslationPhrase = translationPhrase;
        NativeSentence = nativeSentence;
        TranslatedSentence = translatedSentence;
        PhotoUrl = photoUrl;
    }

    public override string ToString() => $"{NativePhrase.Value} - {TranslationPhrase.Value}";

}