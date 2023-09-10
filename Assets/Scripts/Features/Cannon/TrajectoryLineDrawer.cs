using CannonShootingPrototype.Data.Dynamic.Cannon;
using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon
{
    public class TrajectoryLineDrawer : ITickable, IFixedTickable
    {
        private readonly CannonData _cannon;
        private readonly CannonShellConfig _cannonShellConfig;
        private readonly LineRenderer _trajectoryLineRenderer;
        private readonly TrajectoryPredictor _trajectoryPredictor;

        private float _fixedDeltaTime;

        public TrajectoryLineDrawer(CannonData cannon, CannonShellConfig cannonShellConfig,
            LineRenderer trajectoryLineRenderer, TrajectoryPredictor trajectoryPredictor)
        {
            _cannon = cannon;
            _cannonShellConfig = cannonShellConfig;
            _trajectoryLineRenderer = trajectoryLineRenderer;
            _trajectoryPredictor = trajectoryPredictor;
        }

        public void FixedTick(float deltaTime) => _fixedDeltaTime = deltaTime;

        public void Tick(float deltaTime)
        {
            (Vector3[] points, int count) = 
                _trajectoryPredictor.PredictTrajectory(_cannon.Muzzle.forward * _cannon.Firepower,
                    _cannonShellConfig.Mass, _fixedDeltaTime, _cannon.Muzzle.position);

            _trajectoryLineRenderer.positionCount = count;
            _trajectoryLineRenderer.SetPositions(points);
        }
    }
}