using CannonShootingPrototype.Data.Static.Configuration.Creation;
using UnityEngine;

namespace CannonShootingPrototype.Data.Static.Configuration.Cannon
{
    [CreateAssetMenu(fileName = nameof(CannonShellConfig), menuName = "Configuration/Cannon/Shell")]
    public class CannonShellConfig : ScriptableObject
    {
        [field: SerializeField] public PoolConfig PoolConfig { get; private set; }

        [field: SerializeField, Min(default)] public int MaxNumberOfRebounds { get; private set; }
        [field: SerializeField, Min(default)] public float MaxMeshVertexPositionOffset { get; private set; }
        [field: SerializeField, Min(default)] public float Mass { get; private set; }
        [field: SerializeField, Min(default)] public float BounceForce { get; private set; }
    }
}