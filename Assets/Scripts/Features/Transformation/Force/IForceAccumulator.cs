using UnityEngine;

namespace CannonShootingPrototype.Features.Transformation.Force
{
    public interface IForceAccumulator : IForceProvider
    {
        void Accumulate(Vector3 force);
        void Accumulate(float x = default, float y = default, float z = default);
    }
}