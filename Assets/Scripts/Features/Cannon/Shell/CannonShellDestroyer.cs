using System.Collections.Generic;
using CannonShootingPrototype.Data.Dynamic.Cannon;
using CannonShootingPrototype.Features.Transformation.Force;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon.Shell
{
    public class CannonShellDestroyer
    {
        private readonly IDictionary<GameObject, CannonShellData> _cannonShells;
        private readonly IList<IForceAccumulator> _forceAccumulators;

        public CannonShellDestroyer(IDictionary<GameObject, CannonShellData> cannonShells,
            IList<IForceAccumulator> forceAccumulators)
        {
            _cannonShells = cannonShells;
            _forceAccumulators = forceAccumulators;
        }

        public void Destroy(CannonShellData cannonShell)
        {
            _forceAccumulators.Remove(cannonShell.ForceAccumulator);
            _cannonShells.Remove(cannonShell.GameObject);
            cannonShell.GameObject.SetActive(false);
        }
    }
}