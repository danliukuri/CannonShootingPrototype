using CannonShootingPrototype.Data.Static.Configuration.Creation;
using UnityEngine;

namespace CannonShootingPrototype.Data.Static.Configuration.Cannon
{
    [CreateAssetMenu(fileName = nameof(CannonShellConfig), menuName = "Configuration/Cannon/Shell")]
    public class CannonShellConfig : ScriptableObject
    {
        [field: SerializeField] public PoolConfig PoolConfig { get; private set; }

        [field: SerializeField] public float MaxMeshVertexPositionOffset { get; private set; }
    }
}