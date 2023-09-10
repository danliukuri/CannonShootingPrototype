using System.Collections.Generic;
using UnityEngine;

namespace CannonShootingPrototype.Infrastructure.Services.Flow
{
    public class ServicesTicker : MonoBehaviour
    {
        public IList<ITickable> TickableServices { get; set; }
        public IList<ITickable> FixedTickableServices { get; set; }
        
        private void Update()
        {
            for (int i = 0; i < TickableServices.Count; i++)
                TickableServices[i].Tick(Time.deltaTime);
        }
        
        private void FixedUpdate()
        {
            for (int i = 0; i < FixedTickableServices.Count; i++)
                FixedTickableServices[i].Tick(Time.fixedDeltaTime);
        }
    }
}