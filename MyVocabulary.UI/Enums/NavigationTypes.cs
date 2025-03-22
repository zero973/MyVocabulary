namespace MyVocabulary.UI.Enums;

/// <summary>
/// Defines the navigation mode, indicating whether a new entity is being created 
/// or an existing entity is being opened.
/// </summary>
public enum NavigationTypes
{
    
    /// <summary>
    /// Indicates that a new entity is being created.
    /// </summary>
    Create = 0, 

    /// <summary>
    /// Indicates that an existing entity is being opened for viewing or editing.
    /// </summary>
    Open = 1
    
}