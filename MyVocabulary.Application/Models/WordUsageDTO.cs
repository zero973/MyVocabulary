using MyVocabulary.Domain.Entities;

namespace MyVocabulary.Application.Models;

public class WordUsageDTO
{

    public Guid Id { get; set; }

    public TopicDTO Topic { get; set; }

    public Word NativeWord { get; set; }

    public Word TranslationWord { get; set; }

    public string NativeSentence { get; set; }

    public string TranslatedSentence { get; set; }

    public string? PhotoUrl { get; set; }

    public WordUsageDTO(Guid id, TopicDTO topic, Word nativeWord, 
        Word translationWord, string nativeSentence, 
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