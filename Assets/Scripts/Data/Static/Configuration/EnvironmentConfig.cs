using UnityEngine;

namespace CannonShootingPrototype.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(EnvironmentConfig), menuName = "Configuration/Environment")]
    public class EnvironmentConfig : ScriptableObject
    {
        [field: SerializeField] public Vector3 GravityForce { get; private set; }
    }
}