using System;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using UnityEngine;

namespace CannonShootingPrototype.Features.Player
{
    public class PlayerRotator : IInitializable, IDisposable
    {
        private readonly IMouseInputService _mouseInputService;
        private readonly Transform _player;
        private readonly float _rotationSpeed;

        public PlayerRotator(IMouseInputService mouseInputService, Transform player, float rotationSpeed)
        {
            _player = player;
            _rotationSpeed = rotationSpeed;
            _mouseInputService = mouseInputService;
        }

        public void Initialize() => _mouseInputService.AxisXChanged += Rotate;

        public void Dispose() => _mouseInputService.AxisXChanged -= Rotate;

        private void Rotate(float mouseAxisX)
        {
            _player.Rotate(Vector3.up, mouseAxisX * _rotationSpeed);
        }
    }
}