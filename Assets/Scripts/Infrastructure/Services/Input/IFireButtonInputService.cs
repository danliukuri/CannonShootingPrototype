using System;

namespace CannonShootingPrototype.Infrastructure.Services.Input
{
    public interface IFireButtonInputService
    {
        event Action FireButtonPressed;
    }
}