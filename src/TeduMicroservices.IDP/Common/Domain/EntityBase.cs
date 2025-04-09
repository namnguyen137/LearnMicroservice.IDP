namespace LearnMicroservice.IDP.Common.Domain;

public abstract class EntityBase<TKey> : IEntityBase<TKey>
{
    public TKey Id { get; set; }
}
