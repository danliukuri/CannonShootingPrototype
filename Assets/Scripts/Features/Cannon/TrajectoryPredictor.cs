using CannonShootingPrototype.Data.Static.Configuration;
using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon
{
    public class TrajectoryPredictor
    {
        private readonly EnvironmentConfig _environmentConfig;
        private readonly int _maxNumberOfPoints;

        public TrajectoryPredictor(EnvironmentConfig environmentConfig, int maxNumberOfPoints)
        {
            _environmentConfig = environmentConfig;
            _maxNumberOfPoints = maxNumberOfPoints;
        }

        public (Vector3[] Points, int Count) PredictTrajectory(Vector3 velocity, float mass, float deltaTime,
            Vector3 initialPosition)
        {
            var points = new Vector3[_maxNumberOfPoints];
            Vector3 position = initialPosition;
            bool isHit = default;
            int pointsCount = default;

            for (var pointIndex = 0; pointIndex < _maxNumberOfPoints && !isHit; pointIndex++, pointsCount++)
            {
                velocity += _environmentConfig.GravityForce * deltaTime;
                Vector3 nextPosition = position + velocity * (mass * deltaTime);
                
                isHit =
                    Physics.Raycast(position, velocity, out RaycastHit hit, Vector3.Distance(position, nextPosition));
                if (isHit)
                    position = hit.point;
                
                points[pointIndex] = position;
                position = nextPosition;
            }
            return (points, pointsCount);
        }
    }
}