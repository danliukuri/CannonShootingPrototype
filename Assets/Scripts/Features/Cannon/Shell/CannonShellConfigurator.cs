using System.Collections.Generic;
using CannonShootingPrototype.Data.Dynamic.Cannon;
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
        private readonly IDictionary<GameObject, CannonShellData> _cannonShells;
        private readonly FlowServicesContainer _flowServicesContainer;
        private readonly IList<IForceAccumulator> _forceAccumulators;
        private readonly Transform _initialTransform;
        private readonly float _mass;
        private readonly IMeshGenerator _meshGenerator;

        public CannonShellConfigurator(IDictionary<GameObject, CannonShellData> cannonShells,
            FlowServicesContainer flowServicesContainer, IList<IForceAccumulator> forceAccumulators,
            Transform initialTransform, float mass, IMeshGenerator meshGenerator)
        {
            _cannonShells = cannonShells;
            _flowServicesContainer = flowServicesContainer;
            _forceAccumulators = forceAccumulators;
            _initialTransform = initialTransform;
            _meshGenerator = meshGenerator;
            _mass = mass;
        }

        public GameObject Configure(GameObject configurableObject)
        {
            CannonShellData cannonShell = ConfigureData(configurableObject.GetComponent<Transform>());
            _cannonShells.Add(configurableObject, cannonShell);
            ConfigureMesh(configurableObject.GetComponent<MeshFilter>());
            ConfigureMover(cannonShell);
            return configurableObject;
        }

        private CannonShellData ConfigureData(Transform transform)
        {
            var cannonShell = new CannonShellData
            {
                Transform = ConfigureTransform(transform),
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
            var positionChanger = new ObjectMover(cannonShell.ForceAccumulator, cannonShell.Transform, _mass);
            positionChanger.Initialize();
            _flowServicesContainer.DisposableServices.Add(positionChanger);
            _flowServicesContainer.TickableServices.Add(positionChanger);
            return positionChanger;
        }
    }
}