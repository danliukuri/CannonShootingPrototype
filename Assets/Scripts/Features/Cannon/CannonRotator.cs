using System;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon
{
    public class CannonRotator : IInitializable, IDisposable
    {
        private readonly Transform _cannon;
        private readonly IMouseInputService _mouseInputService;
        private readonly Transform _target;
        private readonly float _rotationSpeed;

        public CannonRotator(Transform cannon, IMouseInputService mouseInputService, Transform target,
            float rotationSpeed)
        {
            _cannon = cannon;
            _mouseInputService = mouseInputService;
            _target = target;
            _rotationSpeed = rotationSpeed;
        }

        public void Initialize() => _mouseInputService.AxisXChanged += RotateAroundTarget;

        public void Dispose() => _mouseInputService.AxisXChanged -= RotateAroundTarget;

        private void RotateAroundTarget(float mouseAxisX) =>
            _cannon.RotateAround(_target.position, Vector3.up, mouseAxisX * _rotationSpeed);
    }
}