using System;
using System.Collections.Generic;

namespace CannonShootingPrototype.Infrastructure.Services.Flow
{
    public class FlowServicesContainer
    {
        public List<IInitializable> InitializableServices { get; } = new List<IInitializable>();
        public List<IDisposable> DisposableServices { get; } = new List<IDisposable>();
        public List<ITickable> TickableServices { get; } = new List<ITickable>();
        public List<ITickable> FixedTickableServices { get; } = new List<ITickable>();
    }
}