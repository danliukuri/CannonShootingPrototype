using System;
using CannonShootingPrototype.Architecture.GameStates;
using CannonShootingPrototype.Features.Player;
using CannonShootingPrototype.Infrastructure.Services;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using CannonShootingPrototype.Utilities.Patterns.State;
using CannonShootingPrototype.Utilities.Patterns.State.Containers;
using CannonShootingPrototype.Utilities.Patterns.State.Machines;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    public class CompositionRoot
    {
        private readonly AssetsDependenciesProvider _assetsDependenciesProvider;
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;

        public CompositionRoot(AssetsDependenciesProvider assetsDependenciesProvider,
            SceneDependenciesProvider sceneDependenciesProvider)
        {
            _assetsDependenciesProvider = assetsDependenciesProvider;
            _sceneDependenciesProvider = sceneDependenciesProvider;
        }

        public IStateMachine Initialize()
        {
            var states = new IState[] { new SetupGameState() };
            var stateContainer = new StateContainer();
            var statesContainerInitializer = new StatesContainerInitializer(stateContainer, states);
            var stateMachine = new StateMachine(stateContainer);

            var mouseInputService = new MouseInputService();

            var playerRotator = new PlayerRotator(mouseInputService, _sceneDependenciesProvider.Player,
                _assetsDependenciesProvider.PlayerConfig.RotationSpeed);


            var servicesInitializer = new ServicesInitializer(new IInitializable[]
            {
                statesContainerInitializer, playerRotator
            });
            servicesInitializer.Initialize();

            ServicesTicker servicesTicker = _sceneDependenciesProvider.ServicesTicker;
            servicesTicker.TickableServices = new ITickable[] { mouseInputService };

            ServicesDisposer servicesDisposer = _sceneDependenciesProvider.ServicesDisposer;
            servicesDisposer.DisposableServices = new IDisposable[] { playerRotator };

            return stateMachine;
        }
    }
}