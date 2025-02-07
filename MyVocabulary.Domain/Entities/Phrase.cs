using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// Represents a phrase or word in a specific language (<see cref="Culture"/>).
/// </summary>
public class Phrase : BaseEntity, IAggregateRoot
{

    /// <summary>
    /// The textual representation of the phrase or word.
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// The culture or language of the phrase or word (e.g., "en-US", "fr-FR").
    /// </summary>
    public string Culture { get; private set; }

#pragma warning disable CS8618 // Required by Entity Framework
    private Phrase() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="Phrase"/> class.
    /// </summary>
    /// <param name="value">The textual representation of the phrase or word.</param>
    /// <param name="culture">The culture or language of the phrase or word.</param>
    public Phrase(string value, string culture)
    {
        Value = value.ToLower();
        Culture = culture;
    }

    /// <summary>
    /// Updates the word's details.
    /// </summary>
    /// <param name="value">The updated textual representation of the phrase or word.</param>
    /// <param name="culture">The updated culture or language of the phrase or word.</param>
    public void Edit(string value, string culture)
    {
        Value = value.ToLower();
        Culture = culture;
    }

    public override string ToString() => $"[{Culture}] {Value}";

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        var phrase = (obj as Phrase)!;

        if (Id == phrase.Id) return true;

        if (Value == phrase.Value && Culture == phrase.Culture) return true;

        return false;
    }

}