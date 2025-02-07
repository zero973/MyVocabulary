using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// Represents a specific usage case of a phrase or word (<see cref="NativePhraseId"/>), including its occurrence in a sentence (<see cref="NativeSentence"/>),
/// and its translation (<see cref="TranslationPhraseId"/>) within the context of that sentence.
/// </summary>
public class PhraseUsage : BaseEntity, IAggregateRoot
{

    /// <summary>
    /// The identifier of the linked topic.
    /// </summary>
    public Guid TopicId { get; private set; }

    /// <summary>
    /// The identifier of the phrase or word being studied, as used in <see cref="NativeSentence"/>.
    /// </summary>
    public Guid NativePhraseId { get; private set; }

    /// <summary>
    /// The identifier of the translation of the phrase or word in <see cref="NativeSentence"/>.
    /// </summary>
    public Guid TranslationPhraseId { get; private set; }

    /// <summary>
    /// The original sentence or phrase where the phrase or word (<see cref="NativePhraseId"/>) is used.
    /// </summary>
    public string NativeSentence { get; private set; }

    /// <summary>
    /// The translated version of <see cref="NativeSentence"/> that includes the translation of the phrase or word.
    /// </summary>
    public string TranslatedSentence { get; private set; }

    /// <summary>
    /// An optional URL of an image associated with the phrase or word or sentence.
    /// </summary>
    public string? PhotoUrl { get; private set; }

#pragma warning disable CS8618 // Required by Entity Framework
    private PhraseUsage() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="PhraseUsage"/> class.
    /// </summary>
    /// <param name="topicId">The identifier of the topic associated with this phrase or word usage.</param>
    /// <param name="nativePhraseId">The identifier of the word being studied.</param>
    /// <param name="translationPhraseId">The identifier of the word's translation.</param>
    /// <param name="nativeSentence">The original sentence where the word is used.</param>
    /// <param name="translatedSentence">The translated sentence including the word's translation.</param>
    /// <param name="photoUrl">An optional URL for an image associated with the word or sentence.</param>
    public PhraseUsage(Guid topicId, Guid nativePhraseId, Guid translationPhraseId,
        string nativeSentence, string translatedSentence, string? photoUrl)
    {
        TopicId = topicId;
        NativePhraseId = nativePhraseId;
        TranslationPhraseId = translationPhraseId;
        NativeSentence = nativeSentence;
        TranslatedSentence = translatedSentence;
        PhotoUrl = photoUrl;
    }

    /// <summary>
    /// Updates the properties of this word usage instance.
    /// </summary>
    /// <param name="topicId">The new topic identifier.</param>
    /// <param name="nativePhraseId">The new identifier for the phrase or word being studied.</param>
    /// <param name="translationPhraseId">The new identifier for the word's translation.</param>
    /// <param name="nativeSentence">The updated original sentence.</param>
    /// <param name="translatedSentence">The updated translated sentence.</param>
    /// <param name="photoUrl">The updated URL for the associated image.</param>
    public void Edit(Guid topicId, Guid nativePhraseId, Guid translationPhraseId,
        string nativeSentence, string translatedSentence, string? photoUrl)
    {
        TopicId = topicId;
        NativePhraseId = nativePhraseId;
        TranslationPhraseId = translationPhraseId;
        NativeSentence = nativeSentence;
        TranslatedSentence = translatedSentence;
        PhotoUrl = photoUrl;
    }

}