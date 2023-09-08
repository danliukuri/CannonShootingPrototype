using System;
using CannonShootingPrototype.Infrastructure.Services.Flow;

namespace CannonShootingPrototype.Infrastructure.Services.Input
{
    public class FireButtonInputService : ITickable, IFireButtonInputService
    {
        private const string FireButtonName = "Fire1";

        public event Action FireButtonPressed;

        public void Tick()
        {
            if (UnityEngine.Input.GetButtonDown(FireButtonName))
                FireButtonPressed?.Invoke();
        }
    }
}