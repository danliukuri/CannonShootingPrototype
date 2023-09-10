using CannonShootingPrototype.Data.Dynamic.Cannon;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon.Shell
{
    public class CannonShellBouncingHandler : IBouncingHandler
    {
        private readonly CannonShellData _cannonShellData;

        public CannonShellBouncingHandler(CannonShellData cannonShellData) => _cannonShellData = cannonShellData;

        public void HandleBounce(Vector3 collisionNormal, Vector3 force) => GenerateBounceForce(collisionNormal, force);

        private void GenerateBounceForce(Vector3 collisionNormal, Vector3 force)
        {
            Vector3 reflectedForce = Vector3.Reflect(force, collisionNormal.normalized);
            Vector3 bounceForce = reflectedForce.normalized * (force.magnitude * _cannonShellData.Config.BounceForce);

            _cannonShellData.ForceAccumulator.Accumulate(-force + bounceForce);
        }
    }
}