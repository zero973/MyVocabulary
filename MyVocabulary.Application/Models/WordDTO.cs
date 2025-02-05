namespace MyVocabulary.Application.Models;

public class WordDTO
{

    public Guid Id { get; set; } = Guid.NewGuid();

    public string Value { get; set; }

    public Language Language { get; set; }

    public WordDTO(string value, Language language)
    {
        Value = value.ToLower();
        Language = language;
    }

    public WordDTO(Guid id, string value, Language language) 
        : this(value, language)
    {
        Id = id;
    }

}