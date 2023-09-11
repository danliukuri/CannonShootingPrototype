using UnityEngine;

namespace CannonShootingPrototype.Data.Static.Configuration
{
    [CreateAssetMenu(fileName = nameof(CameraConfig), menuName = "Configuration/Camera")]
    public class CameraConfig : ScriptableObject
    {
        [field: SerializeField] public float ShakeDuration { get; private set; }
        [field: SerializeField] public float ShakeFrequency { get; private set; }
    }
}