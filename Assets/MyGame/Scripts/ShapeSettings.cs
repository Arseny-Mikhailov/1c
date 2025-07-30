using UnityEngine;

namespace MyGame.Scripts
{
    [CreateAssetMenu(fileName = "Shape", menuName = "Scriptable Objects/ShapeSettings")]
    public class ShapeSettings : ScriptableObject
    {
        [Header("Префабы")]
        [field: SerializeField] public GameObject SquarePrefab {get; set;}
        [field: SerializeField] public GameObject CirclePrefab {get; set;}
        [field: SerializeField] public GameObject TrianglePrefab {get; set;}
        [field: SerializeField] public GameObject StarPrefab {get; set;}

        [Header("Настройки скорости")] 
        [field: SerializeField] public float MinSpeed {get; set;}
        [field: SerializeField] public float MaxSpeed {get; set;}

        [Header("Настройки интервала появления")] 
        [field: SerializeField] public float SpawnIntervalMin {get; set;}
        [field: SerializeField] public float SpawnIntervalMax {get; set;}

        [Header("Настройки начальных очков и жизней")] 
        [field: SerializeField] public int InitialLives {get; set;}
        [field: SerializeField] public int ShapesToWin {get; set;}
    }
}