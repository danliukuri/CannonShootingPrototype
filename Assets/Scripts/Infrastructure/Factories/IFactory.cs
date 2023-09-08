namespace CannonShootingPrototype.Infrastructure.Factories
{
    public interface IFactory<out T>
    {
        T Get();
    }
}