using CannonShootingPrototype.Features.MeshGeneration;
using CannonShootingPrototype.Infrastructure.Factories;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon.Shell
{
    public class CannonShellConfigurator : IConfigurator<GameObject>
    {
        private readonly Vector3 _initialPosition;
        private readonly IMeshGenerator _meshGenerator;
        private readonly Transform _initialTransform;

        public CannonShellConfigurator(Transform initialTransform, IMeshGenerator meshGenerator)
        {
            _initialTransform = initialTransform;
            _meshGenerator = meshGenerator;
        }

        public GameObject Configure(GameObject configurableObject)
        {
            ConfigureTransform(configurableObject.GetComponent<Transform>());
            ConfigureMesh(configurableObject.GetComponent<MeshFilter>());
            return configurableObject;
        }

        private void ConfigureTransform(Transform transform)
        {
            transform.position = _initialTransform.position;
            transform.rotation = _initialTransform.rotation;
        }

        private void ConfigureMesh(MeshFilter meshFilter) => meshFilter.mesh = _meshGenerator.Generate();
    }
}