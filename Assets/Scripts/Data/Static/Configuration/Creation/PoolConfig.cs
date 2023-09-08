using UnityEngine;

namespace CannonShootingPrototype.Data.Static.Configuration.Creation
{
    [CreateAssetMenu(fileName = nameof(PoolConfig), menuName = "Configuration/Creation/Pool")]
    public class PoolConfig : ScriptableObject
    {
        [field: SerializeField] public GameObject Prefab { get; private set; }

        [field: SerializeField, Min(default)] public int InitialNumberOfObjects { get; private set; }
    }
}