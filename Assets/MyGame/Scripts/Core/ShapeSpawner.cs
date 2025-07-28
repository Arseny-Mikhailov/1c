using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace MyGame.Scripts.Core
{
    public class ShapeSpawner : MonoBehaviour
    {
        [Inject] private IShapeFactory _shapeFactory;
        [Inject] private ShapeSettings _settings;
    
        private CancellationTokenSource _cancellationTokenSource;

        private void OnDestroy()
        {
            StopSpawning();
        }

        public void StartSpawning()
        {
            StopSpawning();
        
            _cancellationTokenSource = new CancellationTokenSource();
            SpawnRoutine(_cancellationTokenSource.Token).Forget();
        }
    
        public void StopSpawning()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }
    
        private async UniTaskVoid SpawnRoutine(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var delay = Random.Range(_settings.spawnIntervalMin, _settings.spawnIntervalMax);
            
                try
                {
                    await UniTask.Delay((int)(delay * 1000), cancellationToken: token);
                    _shapeFactory.CreateShape();
                }
                catch (System.OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}