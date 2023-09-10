using System;
using System.Collections.Generic;
using CannonShootingPrototype.Data.Dynamic.Cannon;
using CannonShootingPrototype.Infrastructure.Factories;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon.Shell
{
    public class CannonShellCollisionHandler : IInitializable, IDisposable, IFixedTickable
    {
        private readonly IBouncingHandler _bouncingHandler;
        private readonly CannonShellData _cannonShellData;
        private readonly CannonShellDestroyer _cannonShellDestroyer;
        private readonly IFactory<GameObject> _cannonShellCollisionTrailFactory;

        private Vector3 _currentShellForce;
        private bool _isNeededToHandleCollision;
        private int _numberOfRebounds;

        public CannonShellCollisionHandler(IBouncingHandler bouncingHandler, CannonShellData cannonShellData,
            CannonShellDestroyer cannonShellDestroyer, IFactory<GameObject> cannonShellCollisionTrailFactory)
        {
            _bouncingHandler = bouncingHandler;
            _cannonShellDestroyer = cannonShellDestroyer;
            _cannonShellData = cannonShellData;
            _numberOfRebounds = cannonShellData.Config.MaxNumberOfRebounds;
            _cannonShellCollisionTrailFactory = cannonShellCollisionTrailFactory;
        }

        public void Initialize() => _cannonShellData.ForceAccumulator.ForceChanged += HandleCollisionNextFrame;

        public void Dispose() => _cannonShellData.ForceAccumulator.ForceChanged -= HandleCollisionNextFrame;

        public void FixedTick(float deltaTime)
        {
            Vector3 currentShellPosition = _cannonShellData.Transform.position;
            if (_isNeededToHandleCollision && IsCollisionEnter(out Collider collider, currentShellPosition, deltaTime)) 
                HandleCollision(collider, currentShellPosition);
        }

        private void HandleCollisionNextFrame(Vector3 force)
        {
            _currentShellForce = force;
            _isNeededToHandleCollision = true;
        }

        public void HandleCollision(Collider collider, Vector3 position)
        {
            Vector3 collisionPoint = collider.ClosestPoint(position);
            Vector3 collisionNormal = position - collisionPoint;
            if (_numberOfRebounds > 0)
            {
                _bouncingHandler.HandleBounce(collisionNormal, _currentShellForce);
                _numberOfRebounds--;
            }
            else
                _cannonShellDestroyer.Destroy(_cannonShellData, collisionPoint);
            
            SpawnCollisionTrail(collisionPoint, collisionNormal);

            _isNeededToHandleCollision = false;
        }

        private void SpawnCollisionTrail(Vector3 collisionPoint, Vector3 collisionNormal)
        {
            GameObject collisionTrail = _cannonShellCollisionTrailFactory.Get();
            collisionTrail.transform.position = collisionPoint;
            collisionTrail.transform.rotation =
                Quaternion.LookRotation(collisionNormal) * Quaternion.AngleAxis(90f, Vector3.right);
        }

        public bool IsCollisionEnter(out Collider collider, Vector3 position, float deltaTime)
        {
            collider = default;
            foreach ((Ray ray, float maxRaycastDistance) in GenerateRays(_cannonShellData.Transform, position))
                if (Physics.Raycast(ray, out RaycastHit hit, maxRaycastDistance))
                {
                    collider = hit.collider;
                    return true;
                }

            {
                Vector3 nextPosition = position + _currentShellForce * (_cannonShellData.Config.Mass * deltaTime);
                Vector3 direction = nextPosition - position;

                if (Physics.Raycast(new Ray(position, direction), out RaycastHit hit, direction.magnitude))
                {
                    collider = hit.collider;
                    return true;
                }
            }
            return false;
        }

        private IEnumerable<(Ray, float)> GenerateRays(Transform transform, Vector3 position)
        {
            Vector3 localScale = transform.localScale;
            float diagonal1 = localScale.x / 2f;
            float diagonal2 = Mathf.Sqrt(localScale.x * localScale.x + localScale.y * localScale.y) / 2f;
            float contactOffset = _cannonShellData.Config.MaxMeshVertexPositionOffset;
            float diagonal3 = localScale.magnitude / 2f + contactOffset;
            
            Vector3 up = transform.up;
            Vector3 down = -up;
            Vector3 right = transform.right;
            Vector3 left = -right;
            Vector3 forward = transform.forward;
            Vector3 back = -forward;

            return new[]
            {
                (new Ray(position, down   ), diagonal1),
                (new Ray(position, up     ), diagonal1),
                (new Ray(position, left   ), diagonal1),
                (new Ray(position, right  ), diagonal1),
                (new Ray(position, forward), diagonal1),
                (new Ray(position, back   ), diagonal1),

                (new Ray(position, down + left   ), diagonal2),
                (new Ray(position, down + right  ), diagonal2),
                (new Ray(position, down + forward), diagonal2),
                (new Ray(position, down + back   ), diagonal2),

                (new Ray(position, up + left   ), diagonal2),
                (new Ray(position, up + right  ), diagonal2),
                (new Ray(position, up + forward), diagonal2),
                (new Ray(position, up + back   ), diagonal2),

                (new Ray(position, left  + forward), diagonal2),
                (new Ray(position, left  + back   ), diagonal2),
                (new Ray(position, right + forward), diagonal2),
                (new Ray(position, right + back   ), diagonal2),

                (new Ray(position, down + left  + forward), diagonal3),
                (new Ray(position, down + left  + back   ), diagonal3),
                (new Ray(position, down + right + forward), diagonal3),
                (new Ray(position, down + right + back   ), diagonal3),

                (new Ray(position, up + left  + forward), diagonal3),
                (new Ray(position, up + left  + back   ), diagonal3),
                (new Ray(position, up + right + forward), diagonal3),
                (new Ray(position, up + right + back   ), diagonal3),
            };
        }
    }
}