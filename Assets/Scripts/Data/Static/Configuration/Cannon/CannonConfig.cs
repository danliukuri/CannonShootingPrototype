using UnityEngine;

namespace CannonShootingPrototype.Data.Static.Configuration.Cannon
{
    [CreateAssetMenu(fileName = nameof(CannonConfig), menuName = "Configuration/Cannon")]
    public class CannonConfig : ScriptableObject
    {
        [field: SerializeField, Min(default)] public float MinFirepower { get; private set; }
        [field: SerializeField, Min(default)] public float MaxFirepower { get; private set; }
        [field: SerializeField, Min(default)] public float InitialFirepower { get; private set; }
        [field: SerializeField, Min(default)] public float FirepowerChangingSpeed { get; private set; }
        
    }
}