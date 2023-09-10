using System;
using System.Collections.Generic;
using CannonShootingPrototype.Data.Dynamic.Cannon;
using CannonShootingPrototype.Infrastructure.Factories;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon
{
    public class CannonShellShooter : IInitializable, IDisposable
    {
        private readonly CannonData _cannon;
        private readonly IFactory<GameObject> _cannonShellFactory;
        private readonly IDictionary<GameObject, CannonShellData> _cannonShells;
        private readonly IFireButtonInputService _fireButtonInputService;

        public CannonShellShooter(CannonData cannon, IFactory<GameObject> cannonShellFactory,
            IDictionary<GameObject, CannonShellData> cannonShells, IFireButtonInputService fireButtonInputService)
        {
            _cannon = cannon;
            _cannonShells = cannonShells;
            _fireButtonInputService = fireButtonInputService;
            _cannonShellFactory = cannonShellFactory;
        }

        public void Dispose() => _fireButtonInputService.FireButtonPressed -= Shoot;

        public void Initialize() => _fireButtonInputService.FireButtonPressed += Shoot;

        private void Shoot()
        {
            GameObject cannonShell = _cannonShellFactory.Get();
            _cannonShells[cannonShell].ForceAccumulator.Accumulate(_cannon.Muzzle.forward * _cannon.Firepower);
        }
    }
}