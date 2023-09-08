using System;
using CannonShootingPrototype.Infrastructure.Factories;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon
{
    public class CannonShellShooter : IInitializable, IDisposable
    {
        private readonly IFactory<GameObject> _cannonShellFactory;
        private readonly IFireButtonInputService _fireButtonInputService;

        public CannonShellShooter(IFactory<GameObject> cannonShellFactory, IFireButtonInputService fireButtonInputService)
        {
            _fireButtonInputService = fireButtonInputService;
            _cannonShellFactory = cannonShellFactory;
        }

        public void Initialize() => _fireButtonInputService.FireButtonPressed += Shoot;

        public void Dispose() => _fireButtonInputService.FireButtonPressed -= Shoot;

        private void Shoot() => _cannonShellFactory.Get();
    }
}