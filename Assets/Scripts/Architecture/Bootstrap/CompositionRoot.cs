using System;
using CannonShootingPrototype.Architecture.GameStates;
using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using CannonShootingPrototype.Data.Static.Configuration.Creation;
using CannonShootingPrototype.Features.Cannon;
using CannonShootingPrototype.Features.Cannon.Shell;
using CannonShootingPrototype.Features.MeshGeneration;
using CannonShootingPrototype.Features.Player;
using CannonShootingPrototype.Infrastructure.Factories;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using CannonShootingPrototype.Utilities.Patterns.Pool;
using CannonShootingPrototype.Utilities.Patterns.State;
using CannonShootingPrototype.Utilities.Patterns.State.Containers;
using CannonShootingPrototype.Utilities.Patterns.State.Machines;
using Object = UnityEngine.Object;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    public class CompositionRoot
    {
        private readonly AssetsDependenciesProvider _assetsDependenciesProvider;
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;
        private CannonBarrelRotator _cannonBarrelRotator;
        private CannonRotator _cannonRotator;
        private MouseInputService _mouseInputService;
        private PlayerRotator _playerRotator;
        private StateMachine _stateMachine;
        private StatesContainerInitializer _statesContainerInitializer;
        private FireButtonInputService _fireButtonInputService;
        private CannonShellShooter _cannonShellShooter;
        private ObjectPool _cannonShellPool;

        public CompositionRoot(AssetsDependenciesProvider assetsDependenciesProvider,
            SceneDependenciesProvider sceneDependenciesProvider)
        {
            _assetsDependenciesProvider = assetsDependenciesProvider;
            _sceneDependenciesProvider = sceneDependenciesProvider;
        }

        public IStateMachine Initialize()
        {
            InitializeGameStateMachine();
            InitializeInputServices();
            InitializePlayer();
            InitializeCannon();
            InitializeCannonShells();
            InitializeFlowServices();
            return _stateMachine;
        }

        private void InitializeInputServices()
        {
            _mouseInputService = new MouseInputService();
            _fireButtonInputService = new FireButtonInputService();
        }

        private void InitializeGameStateMachine()
        {
            var states = new IState[] { new SetupGameState() };
            var stateContainer = new StateContainer();
            _statesContainerInitializer = new StatesContainerInitializer(stateContainer, states);
            _stateMachine = new StateMachine(stateContainer);
        }

        private void InitializePlayer() =>
            _playerRotator = new PlayerRotator(_mouseInputService, _sceneDependenciesProvider.Player,
                _assetsDependenciesProvider.PlayerConfig.RotationSpeed);

        private void InitializeCannon()
        {
            _cannonRotator = new CannonRotator(_sceneDependenciesProvider.Cannon, _mouseInputService,
                _sceneDependenciesProvider.Player, _assetsDependenciesProvider.PlayerConfig.RotationSpeed);
            _cannonBarrelRotator = new CannonBarrelRotator(_sceneDependenciesProvider.CannonBarrel,
                _mouseInputService, _assetsDependenciesProvider.PlayerConfig.RotationSpeed);
        }

        private void InitializeCannonShells()
        {
            CannonShellConfig cannonShellConfig = _assetsDependenciesProvider.CannonShellConfig;
            
            PoolConfig cannonShellPoolConfig = cannonShellConfig.PoolConfig;
            _cannonShellPool = new ObjectPool(cannonShellPoolConfig.Prefab,
                _sceneDependenciesProvider.CannonShellsParent, cannonShellPoolConfig.InitialNumberOfObjects,
                Object.Instantiate);

            var meshGenerator = new DeformedCubeMeshGenerator(cannonShellConfig.MaxMeshVertexPositionOffset);
            var cannonShellConfigurator =
                new CannonShellConfigurator(_sceneDependenciesProvider.CannonBarrelMuzzle, meshGenerator);

            var cannonShellFactory = new GameObjectFactory(cannonShellConfigurator, _cannonShellPool);
            _cannonShellShooter = new CannonShellShooter(cannonShellFactory, _fireButtonInputService);

        }

        private void InitializeFlowServices()
        {
            var servicesInitializer = new ServicesInitializer(new IInitializable[]
            {
                _statesContainerInitializer, _playerRotator, _cannonRotator, _cannonBarrelRotator, _cannonShellShooter,
                _cannonShellPool
            });
            servicesInitializer.Initialize();

            ServicesTicker servicesTicker = _sceneDependenciesProvider.ServicesTicker;
            servicesTicker.TickableServices = new ITickable[] { _mouseInputService, _fireButtonInputService };

            ServicesDisposer servicesDisposer = _sceneDependenciesProvider.ServicesDisposer;
            servicesDisposer.DisposableServices = new IDisposable[]
            {
                _playerRotator, _cannonRotator, _cannonBarrelRotator, _cannonShellShooter
            };
        }
    }
}