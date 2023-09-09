using CannonShootingPrototype.Features.Transformation.Force;
using UnityEngine;

namespace CannonShootingPrototype.Data.Dynamic.Cannon
{
    public class CannonShellData
    {
        public Transform Transform { get; set; }

        public IForceAccumulator ForceAccumulator { get; set; }
    }
}