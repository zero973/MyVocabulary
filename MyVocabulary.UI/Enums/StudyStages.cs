namespace MyVocabulary.UI.Enums;

/// <summary>
/// Defines the stages of a language learning session.
/// The user progresses through these stages sequentially.
/// </summary>
public enum StudyStages
{
    /// <summary>
    /// The learning stage where the user reviews phrases with translations.
    /// No interaction is required at this stage.
    /// </summary>
    Learning = 0,

    /// <summary>
    /// The first quiz stage where the user selects the correct translation 
    /// for each original phrase from multiple choices.
    /// </summary>
    OriginalQuiz = 1,

    /// <summary>
    /// The second quiz stage where the user selects the correct native phrase 
    /// for each translated phrase from multiple choices.
    /// </summary>
    ReversedQuiz = 2
}