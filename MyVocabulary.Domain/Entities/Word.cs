using MyVocabulary.Domain.Interfaces;

namespace MyVocabulary.Domain.Entities;

/// <summary>
/// Word in the language <see cref="Culture"/>
/// </summary>
public class Word : BaseEntity, IAggregateRoot
{

    /// <summary>
    /// Word
    /// </summary>
    public string Value { get; private set; }

    /// <summary>
    /// Word culture
    /// </summary>
    public string Culture { get; private set; }

#pragma warning disable CS8618 // Required by Entity Framework
    private Word() { }

    public Word(string value, string culture)
    {
        Value = value;
        Culture = culture;
    }

    /// <summary>
    /// Edit word
    /// </summary>
    public void Edit(string value, string culture)
    {
        Value = value;
        Culture = culture;
    }

}