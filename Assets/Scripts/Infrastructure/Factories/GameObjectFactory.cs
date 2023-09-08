using CannonShootingPrototype.Utilities.Patterns.Pool;
using UnityEngine;

namespace CannonShootingPrototype.Infrastructure.Factories
{
    public class GameObjectFactory : IFactory<GameObject>
    {
        private readonly IConfigurator<GameObject> _gameObjectConfigurator;
        private readonly IObjectPool _objectPool;

        public GameObjectFactory(IConfigurator<GameObject> gameObjectConfigurator, IObjectPool objectPool)
        {
            _gameObjectConfigurator = gameObjectConfigurator;
            _objectPool = objectPool;
        }

        public virtual GameObject Get()
        {
            GameObject freeGameObject = _objectPool.GetFreeObject();
            _gameObjectConfigurator.Configure(freeGameObject);
            return freeGameObject;
        }
    }
}