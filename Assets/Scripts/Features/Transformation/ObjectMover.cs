using System;
using CannonShootingPrototype.Features.Transformation.Force;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Features.Transformation
{
    public class ObjectMover : IInitializable, IDisposable, ITickable
    {
        private readonly float _mass;
        private readonly IForceProvider _motionForceProvider;
        private readonly Transform _transform;

        private bool _isNeededToChangePosition;
        private Vector3 _motionForce;

        public ObjectMover(IForceProvider motionForceProvider, Transform transform, float mass)
        {
            _motionForceProvider = motionForceProvider;
            _transform = transform;
            _mass = mass;
        }

        public void Initialize() => _motionForceProvider.ForceChanged += ChangePositionNextFrame;

        public void Dispose() => _motionForceProvider.ForceChanged -= ChangePositionNextFrame;

        public void Tick(float deltaTime)
        {
            if (_isNeededToChangePosition)
                ChangePosition(_motionForce * (_mass * deltaTime));
        }

        private void ChangePositionNextFrame(Vector3 motionForce)
        {
            _motionForce = motionForce;
            _isNeededToChangePosition = true;
        }

        public void ChangePosition(Vector3 motionForce)
        {
            _transform.position += motionForce;
            _isNeededToChangePosition = false;
        }
    }
}