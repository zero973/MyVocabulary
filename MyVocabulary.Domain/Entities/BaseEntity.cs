namespace MyVocabulary.Domain.Entities;

/// <summary>
/// Base entity with ID
/// </summary>
public abstract class BaseEntity
{

    /// <summary>
    /// Entity ID
    /// </summary>
    public Guid Id { get; protected set; } = Guid.NewGuid();

    public override string ToString()
    {
        return $"Id: {Id}";
    }

    public bool Equals(BaseEntity? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BaseEntity)obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

}