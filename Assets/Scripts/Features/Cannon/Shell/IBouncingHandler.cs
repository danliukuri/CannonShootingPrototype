using UnityEngine;

namespace CannonShootingPrototype.Features.Cannon.Shell
{
    public interface IBouncingHandler
    {
        void HandleBounce(Vector3 collisionNormal, Vector3 force);
    }
}