using CannonShootingPrototype.Data.Static.Configuration.Cannon;
using CannonShootingPrototype.Infrastructure.Services.Flow;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon
{
    public class TrajectoryLineDrawer : ITickable, IFixedTickable
    {
        private readonly CannonShellConfig _cannonShellConfig;
        private readonly float _firepower;
        private readonly Transform _muzzleTransform;
        private readonly LineRenderer _trajectoryLineRenderer;
        private readonly TrajectoryPredictor _trajectoryPredictor;

        private float _fixedDeltaTime;

        public TrajectoryLineDrawer(CannonShellConfig cannonShellConfig, float firepower, Transform muzzleTransform,
            LineRenderer trajectoryLineRenderer, TrajectoryPredictor trajectoryPredictor)
        {
            _cannonShellConfig = cannonShellConfig;
            _firepower = firepower;
            _muzzleTransform = muzzleTransform;
            _trajectoryLineRenderer = trajectoryLineRenderer;
            _trajectoryPredictor = trajectoryPredictor;
        }

        public void Tick(float deltaTime)
        {
            (Vector3[] points, int count) =
                _trajectoryPredictor.PredictTrajectory(_muzzleTransform.forward * _firepower, _cannonShellConfig.Mass,
                    _fixedDeltaTime, _muzzleTransform.position);

            _trajectoryLineRenderer.positionCount = count;
            _trajectoryLineRenderer.SetPositions(points);
        }

        public void FixedTick(float deltaTime) => _fixedDeltaTime = deltaTime;
    }
}