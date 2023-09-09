using System.Collections.Generic;
using CannonShootingPrototype.Data.Static.Configuration;
using CannonShootingPrototype.Features.Transformation.Force;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Features.Environment
{
    public class GravityForceGenerator : ITickable
    {
        private readonly EnvironmentConfig _environmentConfig;
        private readonly IList<IForceAccumulator> _forceAccumulators;

        public GravityForceGenerator(EnvironmentConfig environmentConfig,
            IList<IForceAccumulator> forceAccumulators)
        {
            _environmentConfig = environmentConfig;
            _forceAccumulators = forceAccumulators;
        }

        public void Tick() => GenerateGravityForce(Time.deltaTime);

        private void GenerateGravityForce(float deltaTime)
        {
            Vector3 gravityForceToAccumulate = _environmentConfig.GravityForce * deltaTime;
            for (var i = 0; i < _forceAccumulators.Count; i++)
                _forceAccumulators[i].Accumulate(gravityForceToAccumulate);
        }
    }
}