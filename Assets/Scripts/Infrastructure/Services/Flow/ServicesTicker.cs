using UnityEngine;

namespace CannonShootingPrototype.Infrastructure.Services.Flow
{
    public class ServicesTicker : MonoBehaviour
    {
        public ITickable[] TickableServices { get; set; }
        
        private void Update()
        {
            foreach (ITickable tickableService in TickableServices)
                tickableService.Tick();
        }
    }
}