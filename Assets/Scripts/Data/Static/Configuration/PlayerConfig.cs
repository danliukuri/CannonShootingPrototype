using UnityEngine;

namespace CannonShootingPrototype.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(PlayerConfig), menuName = "Configuration/Player")]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}