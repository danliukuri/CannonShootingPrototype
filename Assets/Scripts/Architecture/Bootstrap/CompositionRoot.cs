﻿using System.Collections.Generic;
using System.Linq;
using CannonShootingPrototype.Architecture.GameStates;
using CannonShootingPrototype.Data.Dynamic.Cannon;
using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using CannonShootingPrototype.Data.Static.Configuration.Creation;
using CannonShootingPrototype.Features.Cannon;
using CannonShootingPrototype.Features.Cannon.Shell;
using CannonShootingPrototype.Features.Environment;
using CannonShootingPrototype.Features.MeshGeneration;
using CannonShootingPrototype.Features.Player;
using CannonShootingPrototype.Features.Transformation.Force;
using CannonShootingPrototype.Infrastructure.Factories;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using CannonShootingPrototype.Infrastructure.Services.Input;
using CannonShootingPrototype.Utilities.Patterns.Pool;
using CannonShootingPrototype.Utilities.Patterns.State;
using CannonShootingPrototype.Utilities.Patterns.State.Containers;
using CannonShootingPrototype.Utilities.Patterns.State.Machines;
using UnityEngine;

namespace CannonShootingPrototype.Architecture.Bootstrap
{
    public class CompositionRoot
    {
        private readonly AssetsDependenciesProvider _assetsDependenciesProvider;
        private readonly SceneDependenciesProvider _sceneDependenciesProvider;


        private readonly Dictionary<GameObject, CannonShellData> _cannonShells =
            new Dictionary<GameObject, CannonShellData>();
        private readonly IList<IForceAccumulator> _forceAccumulators =
            new List<ForceAccumulator>().Cast<IForceAccumulator>().ToList();

        private FireButtonInputService _fireButtonInputService;
        private FlowServicesContainer _flowServicesContainer;
        private MouseInputService _mouseInputService;
        private StateMachine _stateMachine;
        private CannonData _cannonData;
        private GameObjectFactory _cannonShellExplosionFactory;
        private GameObjectFactory _cannonShellCollisionTrailFactory;
        private ServicesInitializer _servicesInitializer;
        private CameraShaker _cameraShaker;

        public CompositionRoot(AssetsDependenciesProvider assetsDependenciesProvider,
            SceneDependenciesProvider sceneDependenciesProvider)
        {
            _assetsDependenciesProvider = assetsDependenciesProvider;
            _sceneDependenciesProvider = sceneDependenciesProvider;
        }

        public IStateMachine Initialize()
        {
            _flowServicesContainer = new FlowServicesContainer();
            InitializeGameStateMachine();
            InitializeInputServices();

            InitializePlayer();
            InitializeCannon();
            InitializeTrajectoryLineDrawer();

            InitializeCannonShellExplosion();
            InitializeCannonShellCollisionTrail();
            InitializeCannonShells();

            InitializeEnvironmentForceGenerators();

            InitializeFlowServices();
            _servicesInitializer.Initialize();
            return _stateMachine;
        }

        private void InitializeGameStateMachine()
        {
            var states = new IState[]
            {
                new SetupGameState(_flowServicesContainer, _sceneDependenciesProvider.ServicesTicker)
            };
            var stateContainer = new StateContainer();
            var statesContainerInitializer = new StatesContainerInitializer(stateContainer, states);
            _flowServicesContainer.InitializableServices.Add(statesContainerInitializer);
            _flowServicesContainer.DisposableServices.Add(statesContainerInitializer);

            _stateMachine = new StateMachine(stateContainer);
        }

        private void InitializeInputServices()
        {
            _mouseInputService = new MouseInputService();
            _flowServicesContainer.TickableServices.Add(_mouseInputService);
            _fireButtonInputService = new FireButtonInputService();
            _flowServicesContainer.TickableServices.Add(_fireButtonInputService);
        }

        private void InitializePlayer()
        {
            var playerRotator = new PlayerRotator(_mouseInputService, _sceneDependenciesProvider.Player,
                _assetsDependenciesProvider.PlayerConfig.RotationSpeed);
            _flowServicesContainer.InitializableServices.Add(playerRotator);
            _flowServicesContainer.DisposableServices.Add(playerRotator);
            
            _cameraShaker = new CameraShaker(_assetsDependenciesProvider.CameraConfig,
                _sceneDependenciesProvider.PlayerCamera);
            _flowServicesContainer.TickableServices.Add(_cameraShaker);
        }

        private void InitializeCannon()
        {
            var cannonRotator = new CannonRotator(_sceneDependenciesProvider.Cannon, _mouseInputService,
                _sceneDependenciesProvider.Player, _assetsDependenciesProvider.PlayerConfig.RotationSpeed);
            _flowServicesContainer.InitializableServices.Add(cannonRotator);
            _flowServicesContainer.DisposableServices.Add(cannonRotator);

            var cannonBarrelRotator = new CannonBarrelRotator(_sceneDependenciesProvider.CannonBarrel,
                _mouseInputService, _assetsDependenciesProvider.PlayerConfig.RotationSpeed);
            _flowServicesContainer.InitializableServices.Add(cannonBarrelRotator);
            _flowServicesContainer.DisposableServices.Add(cannonBarrelRotator);

            _cannonData = new CannonData
            {
                Config = _assetsDependenciesProvider.CannonConfig,
                Firepower = _assetsDependenciesProvider.CannonConfig.InitialFirepower,
                Barrel = _sceneDependenciesProvider.CannonBarrel,
                Muzzle = _sceneDependenciesProvider.CannonBarrelMuzzle
            };

            var cannonAnimator = new CannonAnimator(_cannonData, _fireButtonInputService);
            _flowServicesContainer.InitializableServices.Add(cannonAnimator);
            _flowServicesContainer.DisposableServices.Add(cannonAnimator);
            _flowServicesContainer.TickableServices.Add(cannonAnimator);
        }

        private void InitializeTrajectoryLineDrawer()
        {
            var trajectoryPredictor = new TrajectoryPredictor(_assetsDependenciesProvider.EnvironmentConfig,
                _assetsDependenciesProvider.CannonTrajectoryLineConfig.MaxNumberOfPoints);
            
            var trajectoryLineDrawer =
                new TrajectoryLineDrawer(_cannonData, _assetsDependenciesProvider.CannonShellConfig,
                    _sceneDependenciesProvider.CannonTrajectoryLineRenderer, trajectoryPredictor);
            _flowServicesContainer.TickableServices.Add(trajectoryLineDrawer);
            _flowServicesContainer.FixedTickableServices.Add(trajectoryLineDrawer);
        }

        private void InitializeCannonShellExplosion()
        {
            PoolConfig cannonShellExplosionPoolConfig = _assetsDependenciesProvider.CannonShellExplosionPoolConfig;
            var cannonShellExplosionPool = new ObjectPool(cannonShellExplosionPoolConfig.Prefab,
                _sceneDependenciesProvider.CannonShellExplosionsParent,
                cannonShellExplosionPoolConfig.InitialNumberOfObjects, Object.Instantiate);
            _flowServicesContainer.InitializableServices.Add(cannonShellExplosionPool);
            
            _cannonShellExplosionFactory = new GameObjectFactory(cannonShellExplosionPool);
        }

        private void InitializeCannonShellCollisionTrail()
        {
            PoolConfig cannonShellCollisionTrailPoolConfig =
                _assetsDependenciesProvider.CannonShellCollisionTrailPoolConfig;
            var cannonShellExplosionPool = new ObjectPool(cannonShellCollisionTrailPoolConfig.Prefab,
                _sceneDependenciesProvider.CannonShellCollisionTrailsParent,
                cannonShellCollisionTrailPoolConfig.InitialNumberOfObjects, Object.Instantiate);
            _flowServicesContainer.InitializableServices.Add(cannonShellExplosionPool);
            
            _cannonShellCollisionTrailFactory = new GameObjectFactory(cannonShellExplosionPool);
        }

        private void InitializeCannonShells()
        {
            CannonShellConfig cannonShellConfig = _assetsDependenciesProvider.CannonShellConfig;

            PoolConfig cannonShellPoolConfig = cannonShellConfig.PoolConfig;
            var cannonShellPool = new ObjectPool(cannonShellPoolConfig.Prefab,
                _sceneDependenciesProvider.CannonShellsParent, cannonShellPoolConfig.InitialNumberOfObjects,
                Object.Instantiate);
            _flowServicesContainer.InitializableServices.Add(cannonShellPool);

            var meshGenerator = new DeformedCubeMeshGenerator(cannonShellConfig.MaxMeshVertexPositionOffset);
            var cannonShellConfigurator = new CannonShellConfigurator(_cannonShellExplosionFactory, _cannonShells,
                _assetsDependenciesProvider.CannonShellConfig, _flowServicesContainer, _forceAccumulators,
                _sceneDependenciesProvider.CannonBarrelMuzzle, meshGenerator, _cannonShellCollisionTrailFactory,
                _cameraShaker);

            var cannonShellFactory = new GameObjectFactory(cannonShellPool, cannonShellConfigurator);

            var cannonShellShooter =
                new CannonShellShooter(_cannonData, cannonShellFactory, _cannonShells, _fireButtonInputService);
            _flowServicesContainer.InitializableServices.Add(cannonShellShooter);
            _flowServicesContainer.DisposableServices.Add(cannonShellShooter);
            
            var cannonFirepowerChanger =
                new CannonFirepowerChanger(_cannonData, _assetsDependenciesProvider.CannonConfig, _mouseInputService);
            _flowServicesContainer.InitializableServices.Add(cannonFirepowerChanger);
            _flowServicesContainer.DisposableServices.Add(cannonFirepowerChanger);
        }

        private void InitializeEnvironmentForceGenerators()
        {
            var gravityForceGenerator = new GravityForceGenerator(_assetsDependenciesProvider.EnvironmentConfig,
                _forceAccumulators);
            _flowServicesContainer.FixedTickableServices.Add(gravityForceGenerator);
        }

        private void InitializeFlowServices()
        {
            _servicesInitializer = new ServicesInitializer(_flowServicesContainer.InitializableServices);
            
            ServicesDisposer servicesDisposer = _sceneDependenciesProvider.ServicesDisposer;
            _flowServicesContainer.DisposableServices.Add(servicesDisposer);
        }
    }
}