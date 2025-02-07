using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// Represents a user's answer (correct or incorrect) for a specific <see cref="PhraseUsage"/>.
/// </summary>
public class UserAnswer : BaseEntity, IAggregateRoot
{

    /// <summary>
    /// The identifier of the associated phrase usage case.
    /// </summary>
    public Guid PhraseUsageId { get; private set; }

    /// <summary>
    /// Indicates whether the user's answer was correct.
    /// </summary>
    public bool IsRight { get; private set; }

    /// <summary>
    /// The date and time when the answer was recorded.
    /// </summary>
    public DateTimeOffset Date { get; private set; } = DateTimeOffset.Now;

#pragma warning disable CS8618 // Required by Entity Framework
    private UserAnswer() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserAnswer"/> class.
    /// </summary>
    /// <param name="phraseUsageId">The identifier of the associated phrase usage case.</param>
    /// <param name="isRight">A value indicating whether the user's answer was correct.</param>
    public UserAnswer(Guid phraseUsageId, bool isRight)
    {
        PhraseUsageId = phraseUsageId;
        IsRight = isRight;
    }

}