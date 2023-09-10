using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using CannonShootingPrototype.Features.Transformation.Force;
using UnityEngine;

namespace CannonShootingPrototype.Data.Dynamic.Cannon
{
    public class CannonShellData
    {
        public CannonShellConfig Config { get; set; }

        public Transform Transform { get; set; }
        public GameObject GameObject { get; set; }

        public IForceAccumulator ForceAccumulator { get; set; }
    }
}