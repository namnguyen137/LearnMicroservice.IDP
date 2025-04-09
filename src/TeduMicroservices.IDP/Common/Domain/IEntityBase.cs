namespace LearnMicroservice.IDP.Common.Domain;

public interface IEntityBase<T>
{
    T Id { get; set; }
}
