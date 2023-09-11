using CannonShootingPrototype.Data.Static.Configuration;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CannonShootingPrototype.Features.Player
{
    public class CameraShaker : ITickable, ICameraShaker
    {
        private readonly CameraConfig _cameraConfig;
        private readonly Vector3 _cameraInitialPosition;
        private readonly Transform _cameraTransform;
        private bool _isShaking;
        
        private float _shakeTimer;

        public CameraShaker(CameraConfig cameraConfig, Transform cameraTransform)
        {
            _cameraTransform = cameraTransform;
            _cameraConfig = cameraConfig;
            _cameraInitialPosition = _cameraTransform.position;
        }

        public void Tick(float deltaTime)
        {
            if (_isShaking)
                Shake(deltaTime);
        }

        public void StartShake()
        {
            _isShaking = true;
            _shakeTimer = _cameraConfig.ShakeDuration;
        }

        private void Shake(float deltaTime)
        {
            _cameraTransform.position = _cameraInitialPosition + Random.insideUnitSphere * _cameraConfig.ShakeFrequency;
            _shakeTimer -= deltaTime;
            if (_shakeTimer <= 0)
            {
                _cameraTransform.position = _cameraInitialPosition;
                _isShaking = false;
            }
        }
    }
}