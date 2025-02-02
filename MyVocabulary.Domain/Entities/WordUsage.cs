using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// Represents a specific usage case of a word (<see cref="NativeWordId"/>), including its occurrence in a sentence (<see cref="NativeSentence"/>),
/// and its translation (<see cref="TranslationWordId"/>) within the context of that sentence.
/// </summary>
public class WordUsage : BaseEntity, IAggregateRoot
{
    /// <summary>
    /// The identifier of the linked topic.
    /// </summary>
    public Guid TopicId { get; private set; }

    /// <summary>
    /// The identifier of the word being studied, as used in <see cref="NativeSentence"/>.
    /// </summary>
    public Guid NativeWordId { get; private set; }

    /// <summary>
    /// The identifier of the translation of the word in <see cref="NativeSentence"/>.
    /// </summary>
    public Guid TranslationWordId { get; private set; }

    /// <summary>
    /// The original sentence or phrase where the word (<see cref="NativeWordId"/>) is used.
    /// </summary>
    public string NativeSentence { get; private set; }

    /// <summary>
    /// The translated version of <see cref="NativeSentence"/> that includes the translation of the word.
    /// </summary>
    public string TranslatedSentence { get; private set; }

    /// <summary>
    /// An optional URL of an image associated with the word or sentence.
    /// </summary>
    public string? PhotoUrl { get; private set; }

#pragma warning disable CS8618 // Required by Entity Framework
    private WordUsage() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="WordUsage"/> class.
    /// </summary>
    /// <param name="topicId">The identifier of the topic associated with this word usage.</param>
    /// <param name="nativeWordId">The identifier of the word being studied.</param>
    /// <param name="translationWordId">The identifier of the word's translation.</param>
    /// <param name="nativeSentence">The original sentence where the word is used.</param>
    /// <param name="translatedSentence">The translated sentence including the word's translation.</param>
    /// <param name="photoUrl">An optional URL for an image associated with the word or sentence.</param>
    public WordUsage(Guid topicId, Guid nativeWordId, Guid translationWordId,
        string nativeSentence, string translatedSentence, string? photoUrl)
    {
        TopicId = topicId;
        NativeWordId = nativeWordId;
        TranslationWordId = translationWordId;
        NativeSentence = nativeSentence;
        TranslatedSentence = translatedSentence;
        PhotoUrl = photoUrl;
    }

    /// <summary>
    /// Updates the properties of this word usage instance.
    /// </summary>
    /// <param name="topicId">The new topic identifier.</param>
    /// <param name="nativeWordId">The new identifier for the word being studied.</param>
    /// <param name="translationWordId">The new identifier for the word's translation.</param>
    /// <param name="nativeSentence">The updated original sentence.</param>
    /// <param name="translatedSentence">The updated translated sentence.</param>
    /// <param name="photoUrl">The updated URL for the associated image.</param>
    public void Edit(Guid topicId, Guid nativeWordId, Guid translationWordId,
        string nativeSentence, string translatedSentence, string? photoUrl)
    {
        TopicId = topicId;
        NativeWordId = nativeWordId;
        TranslationWordId = translationWordId;
        NativeSentence = nativeSentence;
        TranslatedSentence = translatedSentence;
        PhotoUrl = photoUrl;
    }
}