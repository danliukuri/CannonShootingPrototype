using System;
using CannonShootingPrototype.Data.Dynamic.Cannon;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon
{
    public class CannonAnimator : IInitializable, IDisposable, ITickable
    {
        private readonly CannonData _cannon;
        private readonly Vector3 _cannonBarrelInitialPosition;
        private readonly IFireButtonInputService _fireButtonInputService;

        private bool _isAnimatingShoot;

        public CannonAnimator(CannonData cannon, IFireButtonInputService fireButtonInputService)
        {
            _fireButtonInputService = fireButtonInputService;
            _cannon = cannon;
            _cannonBarrelInitialPosition = _cannon.Barrel.localPosition;
        }

        public void Initialize() => _fireButtonInputService.FireButtonPressed += StartAnimateShoot;

        public void Dispose() => _fireButtonInputService.FireButtonPressed -= StartAnimateShoot;

        public void Tick(float deltaTime)
        {
            if (_isAnimatingShoot)
                AnimateShoot(deltaTime);
        }

        private void StartAnimateShoot() => _isAnimatingShoot = true;

        private void AnimateShoot(float deltaTime)
        {
            _cannon.Barrel.position += -_cannon.Barrel.up * (_cannon.Config.ShootAnimationSpeed * deltaTime);

            float distance = Vector3.Distance(_cannon.Barrel.localPosition, _cannonBarrelInitialPosition);
            if (distance >= _cannon.Config.ShootAnimationDistance)
            {
                _cannon.Barrel.localPosition = _cannonBarrelInitialPosition;
                _isAnimatingShoot = false;
            }
        }
    }
}