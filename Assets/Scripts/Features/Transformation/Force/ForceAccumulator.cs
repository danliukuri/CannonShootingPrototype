using System;
using UnityEngine;

namespace CannonShootingPrototype.Features.Transformation.Force
{
    public class ForceAccumulator : IForceAccumulator
    {
        private Vector3 _force;

        public event Action<Vector3> ForceChanged;

        public void Accumulate(Vector3 force) => Accumulate(force.x, force.y, force.z);

        public void Accumulate(float x = default, float y = default, float z = default)
        {
            _force.x += x;
            _force.y += y;
            _force.z += z;
            ForceChanged?.Invoke(_force);
        }
    }
}