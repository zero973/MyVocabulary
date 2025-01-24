using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// A case of using a word(<see cref="NativeWordId"/>) that consists in sentence(<see cref="Sentence"/>)
/// and a translation(<see cref="TranslationWordId"/>) of the word in that sentence
/// </summary>
public class WordCase : BaseEntity, IAggregateRoot
{

    /// <summary>
    /// Linked topic
    /// </summary>
    public Guid TopicId { get; private set; }

    /// <summary>
    /// Native word that used in <see cref="Sentence"/>
    /// </summary>
    public Guid NativeWordId { get; private set; }

    /// <summary>
    /// Word translation in <see cref="Sentence"/>
    /// </summary>
    public Guid TranslationWordId { get; private set; }

    /// <summary>
    /// A sentence or phrase that uses a word
    /// </summary>
    public string Sentence { get; private set; }

    /// <summary>
    /// Picture of word or sentence
    /// </summary>
    public string? PhotoUrl { get; private set; }

#pragma warning disable CS8618 // Required by Entity Framework
    private WordCase() { }

    public WordCase(Guid topicId, Guid nativeWordId, Guid translationWordId, string sentence, string? photoUrl)
    {
        TopicId = topicId;
        NativeWordId = nativeWordId;
        TranslationWordId = translationWordId;
        Sentence = sentence;
        PhotoUrl = photoUrl;
    }

    /// <summary>
    /// Edit word case
    /// </summary>
    public void Edit(Guid topicId, Guid nativeWordId, Guid translationWordId, string sentence, string? photoUrl)
    {
        TopicId = topicId;
        NativeWordId = nativeWordId;
        TranslationWordId = translationWordId;
        Sentence = sentence;
        PhotoUrl = photoUrl;
    }

}