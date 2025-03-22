namespace MyVocabulary.Domain.Interfaces;

/// <summary>
/// Typed version of the IClonable interface
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ICloneable<out T> where T : class
{
    /// <summary>
    /// Get a copy of an object of type <typeparamref name="T"/>
    /// </summary>
    /// <returns></returns>
    T Clone();
}