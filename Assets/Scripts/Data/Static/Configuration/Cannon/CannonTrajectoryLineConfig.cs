using UnityEngine;

namespace CannonShootingPrototype.Data.Static.Configuration.Cannon
{
    [CreateAssetMenu(fileName = nameof(CannonTrajectoryLineConfig), menuName = "Configuration/Cannon/TrajectoryLine")]
    public class CannonTrajectoryLineConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxNumberOfPoints { get; private set; }
    }
}