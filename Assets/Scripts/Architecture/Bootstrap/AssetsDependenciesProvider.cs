using CannonShootingPrototype.Data.Static.Configuration;
using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using CannonShootingPrototype.Data.Static.Configuration.Creation;
using UnityEngine;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    public class AssetsDependenciesProvider : MonoBehaviour
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        [field: SerializeField] public CannonShellConfig CannonShellConfig { get; private set; }
    }
}