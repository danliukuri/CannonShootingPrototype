using System;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon
{
    public class CannonBarrelRotator : IInitializable, IDisposable
    {
        private readonly Transform _cannonBarrel;
        private readonly IMouseInputService _mouseInputService;
        private readonly float _rotationSpeed;

        public CannonBarrelRotator(Transform cannonBarrel, IMouseInputService mouseInputService, float rotationSpeed)
        {
            _cannonBarrel = cannonBarrel;
            _mouseInputService = mouseInputService;
            _rotationSpeed = rotationSpeed;
        }

        public void Initialize() => _mouseInputService.OnMouseAxisYChanged += RotateVertically;

        public void Dispose() => _mouseInputService.OnMouseAxisYChanged -= RotateVertically;

        private void RotateVertically(float mouseAxisY) =>
            _cannonBarrel.Rotate(Vector3.right, mouseAxisY * _rotationSpeed);
    }
}