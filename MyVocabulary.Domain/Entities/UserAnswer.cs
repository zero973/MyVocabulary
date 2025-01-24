using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// User answer (right/wrong) in <see cref="WordCase"/>
/// </summary>
public class UserAnswer : BaseEntity, IAggregateRoot
{

    /// <summary>
    /// Linked word case
    /// </summary>
    public Guid WordCaseId { get; private set; }

    /// <summary>
    /// Is answer correct
    /// </summary>
    public bool IsRight { get; private set; }

    /// <summary>
    /// Answer date
    /// </summary>
    public DateTimeOffset Date { get; private set; } = DateTimeOffset.Now;

#pragma warning disable CS8618 // Required by Entity Framework
    private UserAnswer() { }

    public UserAnswer(Guid wordCaseId, bool isRight)
    {
        WordCaseId = wordCaseId;
        IsRight = isRight;
    }

}