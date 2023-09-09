using System;
using System.Collections.Generic;
using UnityEngine;

namespace CannonShootingPrototype.Infrastructure.Services.Flow
{
    public class ServicesDisposer : MonoBehaviour, IDisposable
    {
        public IList<IDisposable> DisposableServices { get; set; }

        public void Dispose()
        {
            for (int i = 0; i < DisposableServices.Count; i++)
                DisposableServices[i].Dispose();
        }
    }
}