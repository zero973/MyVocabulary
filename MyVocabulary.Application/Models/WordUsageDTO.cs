namespace MyVocabulary.Application.Models;

public class WordUsageDTO
{

    public Guid Id { get; set; }

    public TopicDTO Topic { get; set; }

    public WordDTO NativeWord { get; set; }

    public WordDTO TranslationWord { get; set; }

    public string NativeSentence { get; set; }

    public string TranslatedSentence { get; set; }

    public string? PhotoUrl { get; set; }

    public WordUsageDTO(Guid id, TopicDTO topic, WordDTO nativeWord,
        WordDTO translationWord, string nativeSentence, 
        string translatedSentence, string? photoUrl)
    {
        Id = id;
        Topic = topic;
        NativeWord = nativeWord;
        TranslationWord = translationWord;
        NativeSentence = nativeSentence;
        TranslatedSentence = translatedSentence;
        PhotoUrl = photoUrl;
    }

}