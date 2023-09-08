using UnityEngine;

namespace CannonShootingPrototype.Utilities.Patterns.Pool
{
    public interface IObjectPool
    {
        void Initialize();
        GameObject GetFreeObject();
        void ReturnToPool(GameObject obj);
        void ReturnToPoolAllObjects();
    }
}