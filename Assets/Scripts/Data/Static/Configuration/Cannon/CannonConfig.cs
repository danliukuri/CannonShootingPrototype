using UnityEngine;

namespace CannonShootingPrototype.Data.Static.Configuration.Cannon
{
    [CreateAssetMenu(fileName = nameof(CannonConfig), menuName = "Configuration/Cannon")]
    public class CannonConfig : ScriptableObject
    {
        [field: SerializeField] public float Firepower { get; private set; }
    }
}