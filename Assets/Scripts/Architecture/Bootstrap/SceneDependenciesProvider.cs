using CannonShootingPrototype.Infrastructure.Services;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    public class SceneDependenciesProvider : MonoBehaviour
    {
        [field: SerializeField] public Transform Player { get; private set; }
        [field: SerializeField] public ServicesTicker ServicesTicker { get; private set; }
        [field: SerializeField] public ServicesDisposer ServicesDisposer { get; private set; }
    }
}