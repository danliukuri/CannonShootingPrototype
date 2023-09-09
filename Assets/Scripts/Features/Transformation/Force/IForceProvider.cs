using System;
using UnityEngine;

namespace CannonShootingPrototype.Features.Transformation.Force
{
    public interface IForceProvider
    {
        event Action<Vector3> ForceChanged;
    }
}