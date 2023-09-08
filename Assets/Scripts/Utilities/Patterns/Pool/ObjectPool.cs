using System;
using System.Collections.Generic;
using System.Linq;
using CannonShootingPrototype.Utilities.Extensions.Unity;
using UnityEngine;

namespace CannonShootingPrototype.Utilities.Patterns.Pool
{
    public class ObjectPool : IObjectPool
    {
        private readonly GameObject _gameObjectPrefab;
        private readonly Transform _objectsParent;
        private readonly int _initialNumberOfObjects;
        private readonly Func<GameObject, Transform, GameObject> _instantiationFunc;

        private List<GameObject> _objects;

        public ObjectPool(GameObject gameObjectPrefab, Transform objectsParent, int initialNumberOfObjects,
            Func<GameObject, Transform, GameObject> instantiationFunc)
        {
            _gameObjectPrefab = gameObjectPrefab;
            _objectsParent = objectsParent;
            _initialNumberOfObjects = initialNumberOfObjects;
            _instantiationFunc = instantiationFunc;
        }

        public void Initialize()
        {
            _objects = new List<GameObject>(_initialNumberOfObjects);
            for (var i = 0; i < _initialNumberOfObjects; i++)
                CreateInactiveGameObject();
        }

        public GameObject GetFreeObject()
        {
            GameObject freeGameObject = _objects.FirstOrDefault(obj => !obj.activeSelf);
            if (freeGameObject == default)
                freeGameObject = CreateInactiveGameObject();
            return freeGameObject;
        }

        public void ReturnToPool(GameObject obj)
        {
            Transform objectTransform = obj.transform;
            if (objectTransform.parent != _objectsParent)
                objectTransform.SetParent(_objectsParent);
            obj.SetActive(false);
        }

        public void ReturnToPoolAllObjects()
        {
            foreach (GameObject obj in _objects)
                ReturnToPool(obj);
        }

        private GameObject CreateInactiveGameObject()
        {
            GameObject inactiveGameObject = _gameObjectPrefab.AsInactive(_instantiationFunc, _objectsParent);
            _objects.Add(inactiveGameObject);
            return inactiveGameObject;
        }
    }
}