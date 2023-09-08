using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    public class SceneDependenciesProvider : MonoBehaviour
    {
        [field: SerializeField] public Transform Player { get; private set; }
        [field: SerializeField] public Transform Cannon { get; private set; }
        [field: SerializeField] public Transform CannonBarrel { get; private set; }

        [field: SerializeField] public ServicesTicker ServicesTicker { get; private set; }
        [field: SerializeField] public ServicesDisposer ServicesDisposer { get; private set; }
    }
}