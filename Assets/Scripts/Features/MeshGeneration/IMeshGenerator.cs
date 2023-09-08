using UnityEngine;

namespace CannonShootingPrototype.Features.MeshGeneration
{
    public interface IMeshGenerator
    {
        Mesh Generate();
    }
}