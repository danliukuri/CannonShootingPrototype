using System.Collections.Generic;
using CannonShootingPrototype.Data.Dynamic.Cannon;
using CannonShootingPrototype.Features.Player;
using CannonShootingPrototype.Features.Transformation.Force;
using CannonShootingPrototype.Infrastructure.Factories;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon.Shell
{
    public class CannonShellDestroyer
    {
        private readonly ICameraShaker _cameraShaker;
        private readonly IFactory<GameObject> _cannonShellExplosionFactory;
        private readonly IDictionary<GameObject, CannonShellData> _cannonShells;
        private readonly IList<IForceAccumulator> _forceAccumulators;

        public CannonShellDestroyer(ICameraShaker cameraShaker, IFactory<GameObject> cannonShellExplosionFactory,
            IDictionary<GameObject, CannonShellData> cannonShells, IList<IForceAccumulator> forceAccumulators)
        {
            _cameraShaker = cameraShaker;
            _cannonShells = cannonShells;
            _forceAccumulators = forceAccumulators;
            _cannonShellExplosionFactory = cannonShellExplosionFactory;
        }

        public void Destroy(CannonShellData cannonShell, Vector3 collisionPoint)
        {
            GameObject explosion = _cannonShellExplosionFactory.Get();
            explosion.transform.position = collisionPoint;
            explosion.transform.rotation = Quaternion.LookRotation(cannonShell.Transform.position - collisionPoint) *
                                           Quaternion.AngleAxis(90f, Vector3.right);

            _forceAccumulators.Remove(cannonShell.ForceAccumulator);
            _cannonShells.Remove(cannonShell.GameObject);
            cannonShell.GameObject.SetActive(false);
            _cameraShaker.StartShake();
        }
    }
}