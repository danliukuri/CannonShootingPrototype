using CannonShootingPrototype.Utilities.Patterns.Pool;
using UnityEngine;

namespace CannonShootingPrototype.Infrastructure.Factories
{
    public class GameObjectFactory : IFactory<GameObject>
    {
        private readonly IConfigurator<GameObject> _gameObjectConfigurator;
        private readonly IObjectPool _objectPool;

        public GameObjectFactory(IObjectPool objectPool, IConfigurator<GameObject> gameObjectConfigurator = default)
        {
            _gameObjectConfigurator = gameObjectConfigurator;
            _objectPool = objectPool;
        }

        public GameObject Get()
        {
            GameObject freeGameObject = _objectPool.GetFreeObject();
            _gameObjectConfigurator?.Configure(freeGameObject);
            freeGameObject.SetActive(true);
            return freeGameObject;
        }
    }
}