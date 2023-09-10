namespace CannonShootingPrototype.Infrastructure.Services.Flow
{
    public interface IFixedTickable
    {
        void FixedTick(float deltaTime);
    }
}