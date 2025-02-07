using System.Globalization;

namespace MyVocabulary.Application.Models;

/// <summary>
/// Represents a language with culture-specific information, such as its native name and culture code.
/// Provides convenient access to language-related properties and a default language setting.
/// </summary>
public class Language
{

    /// <summary>
    /// Native language name ("English", "русский", "polski" and etc).
    /// </summary>
    public string Name => Culture.NativeName;

    /// <summary>
    /// The culture or language of the phrase or word (e.g., "en-US", "fr-FR").
    /// </summary>
    public string Value => Culture.Name;

    /// <summary>
    /// Gets the culture information associated with this language.
    /// </summary>
    public CultureInfo Culture { get; private set; }

    public Language(CultureInfo culture)
    {
        Culture = culture;
    }

    public Language(string culture)
    {
        Culture = new CultureInfo(culture);
    }

    public override string ToString() => Value;

    /// <summary>
    /// Gets the default language instance, which is English ("en").
    /// </summary>
    /// <returns>A <see cref="Language"/> instance representing English.</returns>
    public static Language Default()
    {
        return new Language(new CultureInfo("en"));
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        var language = (obj as Language)!;

        return Culture.Equals(language.Culture);
    }

}