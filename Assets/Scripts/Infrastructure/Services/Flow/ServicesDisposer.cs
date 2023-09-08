using System;
using UnityEngine;

namespace CannonShootingPrototype.Infrastructure.Services.Flow
{
    public class ServicesDisposer : MonoBehaviour, IDisposable
    {
        public IDisposable[] DisposableServices { get; set; }

        public void Dispose()
        {
            foreach (IDisposable disposableService in DisposableServices)
                disposableService.Dispose();
        }
    }
}