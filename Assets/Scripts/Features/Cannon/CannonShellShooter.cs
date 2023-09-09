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
        private readonly IFactory<GameObject> _cannonShellFactory;
        private readonly IDictionary<GameObject, CannonShellData> _cannonShells;
        private readonly IFireButtonInputService _fireButtonInputService;
        private readonly float _firepower;
        private readonly Transform _muzzleTransform;

        public CannonShellShooter(IFactory<GameObject> cannonShellFactory,
            IDictionary<GameObject, CannonShellData> cannonShells, IFireButtonInputService fireButtonInputService,
            float firepower, Transform muzzleTransform)
        {
            _cannonShells = cannonShells;
            _muzzleTransform = muzzleTransform;
            _fireButtonInputService = fireButtonInputService;
            _cannonShellFactory = cannonShellFactory;
            _firepower = firepower;
        }

        public void Initialize() => _fireButtonInputService.FireButtonPressed += Shoot;

        public void Dispose() => _fireButtonInputService.FireButtonPressed -= Shoot;

        private void Shoot()
        {
            GameObject cannonShellGameObject = _cannonShellFactory.Get();
            _cannonShells[cannonShellGameObject].ForceAccumulator
                .Accumulate(_muzzleTransform.forward * _firepower);
        }
    }
}