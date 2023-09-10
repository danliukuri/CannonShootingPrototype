using CannonShootingPrototype.Data.Static.Configuration;
using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using UnityEngine;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    public class AssetsDependenciesProvider : MonoBehaviour
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
        [field: SerializeField] public CannonConfig CannonConfig { get; private set; }
        [field: SerializeField] public CannonTrajectoryLineConfig CannonTrajectoryLineConfig { get; private set; }
        [field: SerializeField] public CannonShellConfig CannonShellConfig { get; private set; }
        [field: SerializeField] public EnvironmentConfig EnvironmentConfig { get; private set; }
    }
}