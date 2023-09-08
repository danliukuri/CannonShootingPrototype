namespace CannonShootingPrototype.Infrastructure.Services.Flow
{
    public class ServicesInitializer : IInitializable
    {
        private readonly IInitializable[] _initializableServices;

        public ServicesInitializer(IInitializable[] initializableServices) =>
            _initializableServices = initializableServices;

        public void Initialize()
        {
            foreach (IInitializable initializableService in _initializableServices)
                initializableService.Initialize();
        }
    }
}