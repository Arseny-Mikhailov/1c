using System;
using MyGame.Scripts.Core;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace MyGame.Scripts.Factory
{
    public class ShapeFactory : IShapeFactory
    {
        private readonly ShapeSettings _settings;
        private readonly Transform[] _spawnPoints;

        public ShapeFactory(ShapeSettings settings, Transform[] spawnPoints)
        {
            _settings = settings;
            _spawnPoints = spawnPoints;
        }

        public void CreateShape()
        {
            var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
            var spawnPosition = randomSpawnPoint.position;

            var randomType = (ShapeType)Random.Range(0, Enum.GetValues(typeof(ShapeType)).Length);

            var randomSpeed = Random.Range(_settings.MinSpeed, _settings.MaxSpeed);

            var prefab = GetPrefabByType(randomType);

            var shapeInstance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, randomSpawnPoint);

            var shapeComponent = shapeInstance.GetComponent<ShapeItem>();
            shapeComponent.Initialize(randomType, randomSpeed);
        }

        private GameObject GetPrefabByType(ShapeType type)
        {
            return type switch
            {
                ShapeType.Square => _settings.SquarePrefab,
                ShapeType.Circle => _settings.CirclePrefab,
                ShapeType.Triangle => _settings.TrianglePrefab,
                ShapeType.Star => _settings.StarPrefab,
                _ => null
            };
        }
    }
}