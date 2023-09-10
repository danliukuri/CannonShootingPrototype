using System.Collections.Generic;
using CannonShootingPrototype.Data.Dynamic.Cannon;
using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using CannonShootingPrototype.Features.MeshGeneration;
using CannonShootingPrototype.Features.Transformation;
using CannonShootingPrototype.Features.Transformation.Force;
using CannonShootingPrototype.Infrastructure.Factories;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon.Shell
{
    public class CannonShellConfigurator : IConfigurator<GameObject>
    {
        private readonly IFactory<GameObject> _cannonShellExplosionFactory;
        private readonly IDictionary<GameObject, CannonShellData> _cannonShells;
        private readonly CannonShellConfig _config;
        private readonly FlowServicesContainer _flowServicesContainer;
        private readonly IList<IForceAccumulator> _forceAccumulators;
        private readonly Transform _initialTransform;
        private readonly IMeshGenerator _meshGenerator;

        public CannonShellConfigurator(IFactory<GameObject> cannonShellExplosionFactory,
            IDictionary<GameObject, CannonShellData> cannonShells, CannonShellConfig config,
            FlowServicesContainer flowServicesContainer, IList<IForceAccumulator> forceAccumulators,
            Transform initialTransform, IMeshGenerator meshGenerator)
        {
            _cannonShellExplosionFactory = cannonShellExplosionFactory;
            _cannonShells = cannonShells;
            _config = config;
            _flowServicesContainer = flowServicesContainer;
            _forceAccumulators = forceAccumulators;
            _initialTransform = initialTransform;
            _meshGenerator = meshGenerator;
        }

        public GameObject Configure(GameObject configurableObject)
        {
            CannonShellData cannonShell = ConfigureData(configurableObject.GetComponent<Transform>());
            _cannonShells.Add(configurableObject, cannonShell);
            ConfigureMesh(configurableObject.GetComponent<MeshFilter>());
            ConfigureMover(cannonShell);
            CannonShellDestroyer destroyer = ConfigureDestroyer();
            ConfigureCollisionHandler(cannonShell, destroyer);
            return configurableObject;
        }

        private CannonShellData ConfigureData(Transform transform)
        {
            var cannonShell = new CannonShellData
            {
                Config = _config,
                Transform = ConfigureTransform(transform),
                GameObject = transform.gameObject,
                ForceAccumulator = ConfigureForceAccumulator()
            };
            return cannonShell;
        }

        private Transform ConfigureTransform(Transform transform)
        {
            transform.position = _initialTransform.position;
            transform.rotation = _initialTransform.rotation;
            return transform;
        }

        private ForceAccumulator ConfigureForceAccumulator()
        {
            var forceAccumulator = new ForceAccumulator();
            _forceAccumulators.Add(forceAccumulator);
            return forceAccumulator;
        }

        private Mesh ConfigureMesh(MeshFilter meshFilter) => meshFilter.mesh = _meshGenerator.Generate();

        private ObjectMover ConfigureMover(CannonShellData cannonShell)
        {
            var positionChanger = new ObjectMover(cannonShell.ForceAccumulator, cannonShell.Transform, _config.Mass);
            positionChanger.Initialize();
            _flowServicesContainer.DisposableServices.Add(positionChanger);
            _flowServicesContainer.FixedTickableServices.Add(positionChanger);
            return positionChanger;
        }

        private CannonShellDestroyer ConfigureDestroyer() =>
            new CannonShellDestroyer(_cannonShellExplosionFactory, _cannonShells, _forceAccumulators);

        private CannonShellCollisionHandler ConfigureCollisionHandler(CannonShellData cannonShell,
            CannonShellDestroyer cannonShellDestroyer)
        {
            var collisionHandler = new CannonShellCollisionHandler(cannonShell, cannonShellDestroyer);
            collisionHandler.Initialize();
            _flowServicesContainer.DisposableServices.Add(collisionHandler);
            _flowServicesContainer.FixedTickableServices.Add(collisionHandler);
            return collisionHandler;
        }
    }
}