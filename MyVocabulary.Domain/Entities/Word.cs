using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// Represents a word in a specific language (<see cref="Culture"/>).
/// </summary>
public class Word : BaseEntity, IAggregateRoot
{

    /// <summary>
    /// The textual representation of the word.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// The culture or language of the word (e.g., "en-US", "fr-FR").
    /// </summary>
    public string Culture { get; private set; }

#pragma warning disable CS8618 // Required by Entity Framework
    private Word() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Word"/> class.
    /// </summary>
    /// <param name="value">The textual representation of the word.</param>
    /// <param name="culture">The culture or language of the word.</param>
    public Word(string value, string culture)
    {
        Value = value;
        Culture = culture;
    }

    /// <summary>
    /// Updates the word's details.
    /// </summary>
    /// <param name="value">The updated textual representation of the word.</param>
    /// <param name="culture">The updated culture or language of the word.</param>
    public void Edit(string value, string culture)
    {
        Value = value;
        Culture = culture;
    }

}