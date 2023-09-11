using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Utilities.Patterns.State;

namespace CannonShootingPrototype.Architecture.GameStates
{
    public class SetupGameState : IEnterableState
    {
        private readonly FlowServicesContainer _flowServicesContainer;
        private readonly ServicesTicker _servicesTicker;

        public SetupGameState(FlowServicesContainer flowServicesContainer, ServicesTicker servicesTicker)
        {
            _flowServicesContainer = flowServicesContainer;
            _servicesTicker = servicesTicker;
        }

        public void Enter()
        {
            _servicesTicker.TickableServices = _flowServicesContainer.TickableServices;
            _servicesTicker.FixedTickableServices = _flowServicesContainer.FixedTickableServices;
        }
    }
}