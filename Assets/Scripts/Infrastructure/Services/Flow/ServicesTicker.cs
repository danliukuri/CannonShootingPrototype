using System.Collections.Generic;
using UnityEngine;

namespace CannonShootingPrototype.Infrastructure.Services.Flow
{
    public class ServicesTicker : MonoBehaviour
    {
        public IList<ITickable> TickableServices { get; set; }
        
        private void Update()
        {
            for (int i = 0; i < TickableServices.Count; i++)
                TickableServices[i].Tick();
        }
    }
}