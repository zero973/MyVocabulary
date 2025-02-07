namespace MyVocabulary.Application.Models;

public class PhraseDTO
{

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Value { get; set; }

    public Language Language { get; set; }

    public PhraseDTO(string value, Language language)
    {
        Value = value.ToLower();
        Language = language;
    }

    public PhraseDTO(Guid id, string value, Language language) 
        : this(value, language)
    {
        Id = id;
    }

    public override string ToString() => Value;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        var phrase = (obj as PhraseDTO)!;

        if (Id == phrase.Id) return true;

        if (Value == phrase.Value && Language == phrase.Language) return true;

        return false;
    }

}