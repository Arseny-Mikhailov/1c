using UnityEngine;

namespace MyGame.Scripts.Core
{
    [CreateAssetMenu(fileName = "Shape", menuName = "Scriptable Objects/ShapeSettings")]
    public class ShapeSettings : ScriptableObject
    {
        [Header("Префабы")]
        public GameObject squarePrefab;    
        public GameObject circlePrefab;  
        public GameObject trianglePrefab; 
        public GameObject starPrefab;     
    
        [Header("Настройки скорости")]
        public float minSpeed = 1f;      
        public float maxSpeed = 5f;
    
        [Header("Настройки интервала появления")]
        public float spawnIntervalMin = 1f;
        public float spawnIntervalMax = 3f;
    
        [Header("Настройки начальных очков и жизней")]
        public int initialLives = 3;
        public int shapesToWin = 10;
    }
}
