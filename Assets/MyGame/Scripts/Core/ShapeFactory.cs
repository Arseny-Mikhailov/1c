using UnityEngine;

namespace MyGame.Scripts.Core
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
        
            var randomType = (ShapeType)Random.Range(0, System.Enum.GetValues(typeof(ShapeType)).Length);
        
            var randomSpeed = Random.Range(_settings.minSpeed, _settings.maxSpeed);
        
            var prefab = GetPrefabByType(randomType);
       
            var shapeInstance = Object.Instantiate(prefab, spawnPosition, Quaternion.identity, randomSpawnPoint);
        
            var shapeComponent = shapeInstance.GetComponent<ShapeItem>();
            shapeComponent.Initialize(randomType, randomSpeed);
        }

        private GameObject GetPrefabByType(ShapeType type)
        {
            return type switch
            {
                ShapeType.Square => _settings.squarePrefab,
                ShapeType.Circle => _settings.circlePrefab,
                ShapeType.Triangle => _settings.trianglePrefab,
                ShapeType.Star => _settings.starPrefab,
                _ => null
            };
        }
    }
}