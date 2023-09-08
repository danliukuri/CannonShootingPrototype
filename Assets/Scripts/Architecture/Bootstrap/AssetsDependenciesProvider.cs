using CannonShootingPrototype.Data.Static.Configuration;
using UnityEngine;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    public class AssetsDependenciesProvider : MonoBehaviour
    {
        [field: SerializeField] public PlayerConfig PlayerConfig { get; private set; }
    }
}