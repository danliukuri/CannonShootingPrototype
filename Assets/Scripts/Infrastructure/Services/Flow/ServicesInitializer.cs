using System.Collections.Generic;

namespace CannonShootingPrototype.Infrastructure.Services.Flow
{
    public class ServicesInitializer : IInitializable
    {
        private readonly IList<IInitializable> _initializableServices;

        public ServicesInitializer(IList<IInitializable> initializableServices) =>
            _initializableServices = initializableServices;

        public void Initialize()
        {
            for (int i = 0; i < _initializableServices.Count; i++)
                _initializableServices[i].Initialize();
        }
    }
}