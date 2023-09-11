using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using UnityEngine;

namespace CannonShootingPrototype.Data.Dynamic.Cannon
{
    public class CannonData
    {
        public CannonConfig Config { get; set; }
        public float Firepower { get; set; }
        public Transform Barrel { get; set; }
        public Transform Muzzle { get; set; }
    }
}