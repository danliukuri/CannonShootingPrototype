namespace CannonShootingPrototype.Infrastructure.Factories
{
    public interface IConfigurator<T>
    {
        T Configure(T configurableObject);
    }
}